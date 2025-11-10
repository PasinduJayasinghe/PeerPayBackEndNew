using Application.Commands.PaymentCommand;
using Application.Dtos;
using Application.Interfaces;
using Application.Services;
using Domain.Classes;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.PaymentCommand
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, PaymentIntentDto>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IStripeService _stripeService;
        private readonly IJobRepository _jobRepository;
        private readonly IUserRepository _userRepository;

        public CreatePaymentCommandHandler(
            IPaymentRepository paymentRepository,
            IStripeService stripeService,
            IJobRepository jobRepository,
            IUserRepository userRepository)
        {
            _paymentRepository = paymentRepository;
            _stripeService = stripeService;
            _jobRepository = jobRepository;
            _userRepository = userRepository;
        }

        public async Task<PaymentIntentDto> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            // Verify job exists
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job == null)
            {
                throw new Exception("Job not found");
            }

            // Check if payment already exists for this job
            var existingPayment = await _paymentRepository.GetByJobIdAsync(request.JobId);
            if (existingPayment != null)
            {
                throw new Exception("Payment already exists for this job");
            }

            // Verify users exist
            var employerExists = await _userRepository.ExistsAsync(request.EmployerId);
            var studentExists = await _userRepository.ExistsAsync(request.StudentId);

            if (!employerExists || !studentExists)
            {
                throw new Exception("Employer or Student not found");
            }

            // Create Stripe Payment Intent
            var paymentIntentId = await _stripeService.CreatePaymentIntentAsync(
                request.Amount,
                request.Currency,
                request.JobId,
                request.EmployerId,
                request.StudentId
            );

            // Get payment intent details to get client secret
            var paymentIntentDetails = await _stripeService.GetPaymentIntentAsync(paymentIntentId);
            var clientSecret = ((dynamic)paymentIntentDetails).ClientSecret;

            // Create payment record
            var payment = new Payment
            {
                PaymentId = Guid.NewGuid().ToString(),
                JobId = request.JobId,
                EmployerId = request.EmployerId,
                StudentId = request.StudentId,
                Amount = request.Amount,
                Status = PaymentStatus.Pending,
                TransactionId = paymentIntentId,
                PaymentMethod = PaymentMethod.CreditCard,
                Notes = request.Notes ?? string.Empty,
                GatewayResponse = "Payment Intent Created"
            };

            await _paymentRepository.AddAsync(payment);

            return new PaymentIntentDto
            {
                PaymentIntentId = paymentIntentId,
                ClientSecret = clientSecret,
                Amount = request.Amount,
                Currency = request.Currency,
                Status = "pending"
            };
        }
    }

    public class ConfirmPaymentCommandHandler : IRequestHandler<ConfirmPaymentCommand, PaymentDto>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IStripeService _stripeService;
        private readonly INotificationService _notificationService;

        public ConfirmPaymentCommandHandler(
            IPaymentRepository paymentRepository,
            IStripeService stripeService,
            INotificationService notificationService)
        {
            _paymentRepository = paymentRepository;
            _stripeService = stripeService;
            _notificationService = notificationService;
        }

        public async Task<PaymentDto> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId);
            if (payment == null)
            {
                throw new Exception("Payment not found");
            }

            // Verify payment with Stripe
            var isConfirmed = await _stripeService.ConfirmPaymentAsync(request.PaymentIntentId);

            if (!isConfirmed)
            {
                throw new Exception("Payment confirmation failed");
            }

            // Update payment status
            payment.Status = PaymentStatus.Completed;
            payment.CompletedDate = DateTime.UtcNow;
            payment.GatewayResponse = "Payment Confirmed";

            await _paymentRepository.UpdateAsync(payment);

            // Send notifications
            await _notificationService.NotifyPaymentReceivedAsync(
                payment.StudentId,
                payment.Amount,
                payment.Job?.Title ?? "Job"
            );

            await _notificationService.NotifyPaymentSentAsync(
                payment.EmployerId,
                payment.Amount,
                payment.Job?.Title ?? "Job"
            );

            return new PaymentDto
            {
                PaymentId = payment.PaymentId,
                JobId = payment.JobId,
                JobTitle = payment.Job?.Title ?? "Unknown",
                EmployerId = payment.EmployerId,
                EmployerName = payment.Employer?.User?.Name ?? "Unknown",
                StudentId = payment.StudentId,
                StudentName = payment.Student?.User?.Name ?? "Unknown",
                Amount = payment.Amount,
                Status = payment.Status,
                CreatedDate = payment.CreatedDate,
                CompletedDate = payment.CompletedDate,
                TransactionId = payment.TransactionId,
                PaymentMethod = payment.PaymentMethod,
                Notes = payment.Notes
            };
        }
    }

    public class RefundPaymentCommandHandler : IRequestHandler<RefundPaymentCommand, bool>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IStripeService _stripeService;

        public RefundPaymentCommandHandler(
            IPaymentRepository paymentRepository,
            IStripeService stripeService)
        {
            _paymentRepository = paymentRepository;
            _stripeService = stripeService;
        }

        public async Task<bool> Handle(RefundPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId);
            if (payment == null)
            {
                throw new Exception("Payment not found");
            }

            if (payment.Status != PaymentStatus.Completed)
            {
                throw new Exception("Only completed payments can be refunded");
            }

            // Create refund in Stripe
            var refundId = await _stripeService.CreateRefundAsync(
                payment.TransactionId,
                request.RefundAmount
            );

            // Update payment status
            payment.Status = PaymentStatus.Refunded;
            payment.Notes = $"{payment.Notes}\nRefund Reason: {request.Reason}";
            payment.GatewayResponse = $"Refunded - {refundId}";

            await _paymentRepository.UpdateAsync(payment);

            return true;
        }
    }

    public class CancelPaymentCommandHandler : IRequestHandler<CancelPaymentCommand, bool>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IStripeService _stripeService;

        public CancelPaymentCommandHandler(
            IPaymentRepository paymentRepository,
            IStripeService stripeService)
        {
            _paymentRepository = paymentRepository;
            _stripeService = stripeService;
        }

        public async Task<bool> Handle(CancelPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId);
            if (payment == null)
            {
                throw new Exception("Payment not found");
            }

            if (payment.Status != PaymentStatus.Pending)
            {
                throw new Exception("Only pending payments can be cancelled");
            }

            // Cancel in Stripe
            var cancelled = await _stripeService.CancelPaymentIntentAsync(payment.TransactionId);

            if (!cancelled)
            {
                throw new Exception("Failed to cancel payment in Stripe");
            }

            // Update payment status
            payment.Status = PaymentStatus.Failed;
            payment.GatewayResponse = "Payment Cancelled";

            await _paymentRepository.UpdateAsync(payment);

            return true;
        }
    }
}
