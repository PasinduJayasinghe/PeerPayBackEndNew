using Application.Dtos;
using Application.Interfaces;
using Application.Queries.PaymentQuery;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.PaymentQuery
{
    public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, PaymentDto?>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentByIdQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentDto?> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId);
            if (payment == null)
            {
                return null;
            }

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

    public class GetEmployerPaymentsQueryHandler : IRequestHandler<GetEmployerPaymentsQuery, PaymentListDto>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetEmployerPaymentsQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentListDto> Handle(GetEmployerPaymentsQuery request, CancellationToken cancellationToken)
        {
            var payments = await _paymentRepository.GetByEmployerIdAsync(request.EmployerId);

            var paymentDtos = payments.Select(p => new PaymentDto
            {
                PaymentId = p.PaymentId,
                JobId = p.JobId,
                JobTitle = p.Job?.Title ?? "Unknown",
                EmployerId = p.EmployerId,
                EmployerName = p.Employer?.User?.Name ?? "Unknown",
                StudentId = p.StudentId,
                StudentName = p.Student?.User?.Name ?? "Unknown",
                Amount = p.Amount,
                Status = p.Status,
                CreatedDate = p.CreatedDate,
                CompletedDate = p.CompletedDate,
                TransactionId = p.TransactionId,
                PaymentMethod = p.PaymentMethod,
                Notes = p.Notes
            }).ToList();

            return new PaymentListDto
            {
                Payments = paymentDtos,
                TotalCount = paymentDtos.Count,
                TotalAmount = paymentDtos.Sum(p => p.Amount)
            };
        }
    }

    public class GetStudentPaymentsQueryHandler : IRequestHandler<GetStudentPaymentsQuery, PaymentListDto>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetStudentPaymentsQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentListDto> Handle(GetStudentPaymentsQuery request, CancellationToken cancellationToken)
        {
            var payments = await _paymentRepository.GetByStudentIdAsync(request.StudentId);

            var paymentDtos = payments.Select(p => new PaymentDto
            {
                PaymentId = p.PaymentId,
                JobId = p.JobId,
                JobTitle = p.Job?.Title ?? "Unknown",
                EmployerId = p.EmployerId,
                EmployerName = p.Employer?.User?.Name ?? "Unknown",
                StudentId = p.StudentId,
                StudentName = p.Student?.User?.Name ?? "Unknown",
                Amount = p.Amount,
                Status = p.Status,
                CreatedDate = p.CreatedDate,
                CompletedDate = p.CompletedDate,
                TransactionId = p.TransactionId,
                PaymentMethod = p.PaymentMethod,
                Notes = p.Notes
            }).ToList();

            return new PaymentListDto
            {
                Payments = paymentDtos,
                TotalCount = paymentDtos.Count,
                TotalAmount = paymentDtos.Sum(p => p.Amount)
            };
        }
    }

    public class GetPaymentByJobQueryHandler : IRequestHandler<GetPaymentByJobQuery, PaymentDto?>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentByJobQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentDto?> Handle(GetPaymentByJobQuery request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByJobIdAsync(request.JobId);
            if (payment == null)
            {
                return null;
            }

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

    public class GetPaymentsByStatusQueryHandler : IRequestHandler<GetPaymentsByStatusQuery, IEnumerable<PaymentDto>>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentsByStatusQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<PaymentDto>> Handle(GetPaymentsByStatusQuery request, CancellationToken cancellationToken)
        {
            var payments = await _paymentRepository.GetByStatusAsync(request.Status);

            return payments.Select(p => new PaymentDto
            {
                PaymentId = p.PaymentId,
                JobId = p.JobId,
                JobTitle = p.Job?.Title ?? "Unknown",
                EmployerId = p.EmployerId,
                EmployerName = p.Employer?.User?.Name ?? "Unknown",
                StudentId = p.StudentId,
                StudentName = p.Student?.User?.Name ?? "Unknown",
                Amount = p.Amount,
                Status = p.Status,
                CreatedDate = p.CreatedDate,
                CompletedDate = p.CompletedDate,
                TransactionId = p.TransactionId,
                PaymentMethod = p.PaymentMethod,
                Notes = p.Notes
            }).ToList();
        }
    }

    public class GetPaymentIntentStatusQueryHandler : IRequestHandler<GetPaymentIntentStatusQuery, object>
    {
        private readonly IStripeService _stripeService;

        public GetPaymentIntentStatusQueryHandler(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }

        public async Task<object> Handle(GetPaymentIntentStatusQuery request, CancellationToken cancellationToken)
        {
            return await _stripeService.GetPaymentIntentAsync(request.PaymentIntentId);
        }
    }
}
