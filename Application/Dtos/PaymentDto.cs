using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class PaymentDto
    {
        public string PaymentId { get; set; }
        public string JobId { get; set; }
        public string JobTitle { get; set; }
        public string EmployerId { get; set; }
        public string EmployerName { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string TransactionId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string Notes { get; set; }
    }

    public class CreatePaymentDto
    {
        public string JobId { get; set; }
        public string EmployerId { get; set; }
        public string StudentId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "usd";
        public string Notes { get; set; }
    }

    public class PaymentIntentDto
    {
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
    }

    public class ConfirmPaymentDto
    {
        public string PaymentId { get; set; }
        public string PaymentIntentId { get; set; }
    }

    public class RefundPaymentDto
    {
        public string PaymentId { get; set; }
        public decimal? RefundAmount { get; set; }
        public string Reason { get; set; }
    }

    public class PaymentListDto
    {
        public IEnumerable<PaymentDto> Payments { get; set; }
        public int TotalCount { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class StripeWebhookDto
    {
        public string Type { get; set; }
        public object Data { get; set; }
    }
}
