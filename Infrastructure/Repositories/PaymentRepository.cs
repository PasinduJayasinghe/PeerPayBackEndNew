using Application.Interfaces;
using Domain.Classes;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PeerPayDbContext _context;

        public PaymentRepository(PeerPayDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> GetByIdAsync(string paymentId)
        {
            return await _context.Payments
                .Include(p => p.Job)
                .Include(p => p.Employer)
                .Include(p => p.Student)
                .Include(p => p.Transactions)
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId);
        }

        public async Task<IEnumerable<Payment>> GetByEmployerIdAsync(string employerId)
        {
            return await _context.Payments
                .Where(p => p.EmployerId == employerId)
                .Include(p => p.Job)
                .Include(p => p.Student)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetByStudentIdAsync(string studentId)
        {
            return await _context.Payments
                .Where(p => p.StudentId == studentId)
                .Include(p => p.Job)
                .Include(p => p.Employer)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<Payment> GetByJobIdAsync(string jobId)
        {
            return await _context.Payments
                .Include(p => p.Job)
                .Include(p => p.Employer)
                .Include(p => p.Student)
                .FirstOrDefaultAsync(p => p.JobId == jobId);
        }

        public async Task<Payment> GetByTransactionIdAsync(string transactionId)
        {
            return await _context.Payments
                .Include(p => p.Job)
                .Include(p => p.Employer)
                .Include(p => p.Student)
                .FirstOrDefaultAsync(p => p.TransactionId == transactionId);
        }

        public async Task<IEnumerable<Payment>> GetByStatusAsync(PaymentStatus status)
        {
            return await _context.Payments
                .Where(p => p.Status == status)
                .Include(p => p.Job)
                .Include(p => p.Employer)
                .Include(p => p.Student)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<Payment> AddAsync(Payment payment)
        {
            payment.CreatedDate = DateTime.UtcNow;
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string paymentId)
        {
            return await _context.Payments.AnyAsync(p => p.PaymentId == paymentId);
        }
    }
}
