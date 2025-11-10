using Application.Commands.OTPCommand;
using Application.Dtos;
using Application.Interfaces;
using MediatR;

namespace Application.Commands.OTPCommand
{
    public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, OtpResponseDto>
    {
        private readonly IOTPRepository _otpRepository;

        public VerifyOtpCommandHandler(IOTPRepository otpRepository)
        {
            _otpRepository = otpRepository;
        }

        public async Task<OtpResponseDto> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            // Get OTP by email
            var otp = await _otpRepository.GetOtpByEmailAsync(request.Email);

            if (otp == null)
            {
                throw new Exception("No OTP found for this email");
            }

            // Check if OTP is expired
            if (DateTime.UtcNow > otp.ExpiresAt)
            {
                throw new Exception("OTP has expired. Please request a new one");
            }

            // Check if OTP is already used
            if (otp.IsUsed)
            {
                throw new Exception("OTP has already been used");
            }

            // Verify OTP code
            if (otp.OtpCode != request.Otp)
            {
                // Increment attempts
                otp.Attempts++;
                
                if (otp.Attempts >= 3)
                {
                    throw new Exception("Maximum attempts exceeded. Please request a new OTP");
                }

                throw new Exception("Invalid OTP code");
            }

            // Mark OTP as used
            await _otpRepository.MarkAsUsedAsync(otp.OtpId);

            return new OtpResponseDto
            {
                Success = true,
                Message = "Email verified successfully",
                ExpiresAt = null
            };
        }
    }
}
