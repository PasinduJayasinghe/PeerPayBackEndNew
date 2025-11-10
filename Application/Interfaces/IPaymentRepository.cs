using Domain.Classes;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment> GetByIdAsync(string paymentId);
        Task<IEnumerable<Payment>> GetByEmployerIdAsync(string employerId);
        Task<IEnumerable<Payment>> GetByStudentIdAsync(string studentId);
        Task<Payment> GetByJobIdAsync(string jobId);
        Task<Payment> GetByTransactionIdAsync(string transactionId);
        Task<IEnumerable<Payment>> GetByStatusAsync(PaymentStatus status);
        Task<Payment> AddAsync(Payment payment);
        Task UpdateAsync(Payment payment);
        Task<bool> ExistsAsync(string paymentId);
    }

    public interface IStripeService
    {
        Task<string> CreatePaymentIntentAsync(decimal amount, string currency, string jobId, string employerId, string studentId);
        Task<bool> ConfirmPaymentAsync(string paymentIntentId);
        Task<string> CreateRefundAsync(string paymentIntentId, decimal? amount = null);
        Task<object> GetPaymentIntentAsync(string paymentIntentId);
        Task<bool> CancelPaymentIntentAsync(string paymentIntentId);
    }
}
