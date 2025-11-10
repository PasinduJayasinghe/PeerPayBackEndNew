using Application.Interfaces;
using Domain.Classes;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OTPRepository : IOTPRepository
    {
        private readonly PeerPayDbContext _context;

        public OTPRepository(PeerPayDbContext context)
        {
            _context = context;
        }

        public async Task<OTPVerification> CreateOtpAsync(OTPVerification otp)
        {
            await _context.OTPVerifications.AddAsync(otp);
            await _context.SaveChangesAsync();
            return otp;
        }

        public async Task<OTPVerification> GetOtpByEmailAsync(string email)
        {
            return await _context.OTPVerifications
                .Where(o => o.ContactMethod == email && !o.IsUsed)
                .OrderByDescending(o => o.ExpiresAt)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> MarkAsUsedAsync(string otpId)
        {
            var otp = await _context.OTPVerifications.FindAsync(otpId);
            if (otp == null) return false;

            otp.IsUsed = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteExpiredOtpsAsync()
        {
            var expiredOtps = await _context.OTPVerifications
                .Where(o => o.ExpiresAt < DateTime.UtcNow)
                .ToListAsync();

            _context.OTPVerifications.RemoveRange(expiredOtps);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
