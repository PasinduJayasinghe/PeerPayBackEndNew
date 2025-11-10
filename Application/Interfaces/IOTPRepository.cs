using Domain.Classes;

namespace Application.Interfaces
{
    public interface IOTPRepository
    {
        Task<OTPVerification> CreateOtpAsync(OTPVerification otp);
        Task<OTPVerification> GetOtpByEmailAsync(string email);
        Task<bool> MarkAsUsedAsync(string otpId);
        Task<bool> DeleteExpiredOtpsAsync();
    }
}
