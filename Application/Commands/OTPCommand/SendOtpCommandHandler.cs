using Application.Commands.OTPCommand;
using Application.Dtos;
using Application.Interfaces;
using Domain.Classes;
using Domain.Enums;
using MediatR;

namespace Application.Commands.OTPCommand
{
    public class SendOtpCommandHandler : IRequestHandler<SendOtpCommand, OtpResponseDto>
    {
        private readonly IOTPRepository _otpRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public SendOtpCommandHandler(
            IOTPRepository otpRepository,
            IUserRepository userRepository,
            IEmailService emailService)
        {
            _otpRepository = otpRepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<OtpResponseDto> Handle(SendOtpCommand request, CancellationToken cancellationToken)
        {
            // Check if user exists with this email
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user != null)
            {
                throw new Exception("User with this email already exists");
            }

            // Generate 6-digit OTP
            var random = new Random();
            var otpCode = random.Next(100000, 999999).ToString();

            // Create OTP record
            var otp = new OTPVerification
            {
                OtpId = Guid.NewGuid().ToString(),
                UserId = null, // No user yet during registration
                OtpCode = otpCode,
                Purpose = OTPPurpose.Registration,
                ContactMethod = request.Email,
                IsUsed = false,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                Attempts = 0
            };

            await _otpRepository.CreateOtpAsync(otp);

            // Send OTP via email
            var emailSent = await _emailService.SendOtpEmailAsync(request.Email, otpCode);

            if (!emailSent)
            {
                throw new Exception("Failed to send OTP email");
            }

            return new OtpResponseDto
            {
                Success = true,
                Message = $"OTP sent to {request.Email}",
                ExpiresAt = otp.ExpiresAt
            };
        }
    }
}
