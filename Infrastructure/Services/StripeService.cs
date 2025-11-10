using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class StripeService : IStripeService
    {
        private readonly string _secretKey;

        public StripeService(IConfiguration configuration)
        {
            _secretKey = configuration["Stripe:SecretKey"];
            StripeConfiguration.ApiKey = _secretKey;
        }

        public async Task<string> CreatePaymentIntentAsync(
            decimal amount, 
            string currency, 
            string jobId, 
            string employerId, 
            string studentId)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), // Convert to cents
                Currency = currency.ToLower(),
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
                Metadata = new Dictionary<string, string>
                {
                    { "jobId", jobId },
                    { "employerId", employerId },
                    { "studentId", studentId }
                },
                Description = $"Payment for Job {jobId}"
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            return paymentIntent.Id;
        }

        public async Task<bool> ConfirmPaymentAsync(string paymentIntentId)
        {
            try
            {
                var service = new PaymentIntentService();
                var paymentIntent = await service.GetAsync(paymentIntentId);

                return paymentIntent.Status == "succeeded";
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> CreateRefundAsync(string paymentIntentId, decimal? amount = null)
        {
            var options = new RefundCreateOptions
            {
                PaymentIntent = paymentIntentId
            };

            if (amount.HasValue)
            {
                options.Amount = (long)(amount.Value * 100); // Convert to cents
            }

            var service = new RefundService();
            var refund = await service.CreateAsync(options);

            return refund.Id;
        }

        public async Task<object> GetPaymentIntentAsync(string paymentIntentId)
        {
            var service = new PaymentIntentService();
            var paymentIntent = await service.GetAsync(paymentIntentId);

            return new
            {
                Id = paymentIntent.Id,
                Amount = paymentIntent.Amount / 100m, // Convert from cents
                Currency = paymentIntent.Currency,
                Status = paymentIntent.Status,
                ClientSecret = paymentIntent.ClientSecret,
                Created = paymentIntent.Created,
                Metadata = paymentIntent.Metadata
            };
        }

        public async Task<bool> CancelPaymentIntentAsync(string paymentIntentId)
        {
            try
            {
                var service = new PaymentIntentService();
                var paymentIntent = await service.CancelAsync(paymentIntentId);

                return paymentIntent.Status == "canceled";
            }
            catch
            {
                return false;
            }
        }
    }
}
