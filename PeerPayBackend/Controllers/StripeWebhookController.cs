using Application.Interfaces;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PeerPayBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StripeWebhookController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;

        public StripeWebhookController(
            IPaymentRepository paymentRepository,
            INotificationService notificationService,
            IConfiguration configuration)
        {
            _paymentRepository = paymentRepository;
            _notificationService = notificationService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var webhookSecret = _configuration["Stripe:WebhookSecret"];

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    webhookSecret
                );

                // Handle the event
                if (stripeEvent.Type == "payment_intent.succeeded")
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    await HandlePaymentIntentSucceeded(paymentIntent);
                }
                else if (stripeEvent.Type == "payment_intent.payment_failed")
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    await HandlePaymentIntentFailed(paymentIntent);
                }
                else if (stripeEvent.Type == "charge.refunded")
                {
                    var charge = stripeEvent.Data.Object as Charge;
                    await HandleChargeRefunded(charge);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        private async Task HandlePaymentIntentSucceeded(PaymentIntent paymentIntent)
        {
            var payment = await _paymentRepository.GetByTransactionIdAsync(paymentIntent.Id);
            if (payment != null && payment.Status == PaymentStatus.Pending)
            {
                payment.Status = PaymentStatus.Completed;
                payment.CompletedDate = DateTime.UtcNow;
                payment.GatewayResponse = "Payment succeeded via webhook";

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
            }
        }

        private async Task HandlePaymentIntentFailed(PaymentIntent paymentIntent)
        {
            var payment = await _paymentRepository.GetByTransactionIdAsync(paymentIntent.Id);
            if (payment != null)
            {
                payment.Status = PaymentStatus.Failed;
                payment.GatewayResponse = $"Payment failed: {paymentIntent.LastPaymentError?.Message}";

                await _paymentRepository.UpdateAsync(payment);
            }
        }

        private async Task HandleChargeRefunded(Charge charge)
        {
            var payment = await _paymentRepository.GetByTransactionIdAsync(charge.PaymentIntentId);
            if (payment != null)
            {
                payment.Status = PaymentStatus.Refunded;
                payment.GatewayResponse = "Payment refunded via webhook";

                await _paymentRepository.UpdateAsync(payment);
            }
        }
    }
}
