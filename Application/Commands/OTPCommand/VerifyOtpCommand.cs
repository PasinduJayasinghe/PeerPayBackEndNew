using Application.Dtos;
using FluentValidation;
using MediatR;

namespace Application.Commands.OTPCommand
{
    public class VerifyOtpCommand : IRequest<OtpResponseDto>
    {
        public string Email { get; set; }
        public string Otp { get; set; }
    }

    public class VerifyOtpCommandValidator : AbstractValidator<VerifyOtpCommand>
    {
        public VerifyOtpCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Otp)
                .NotEmpty().WithMessage("OTP is required")
                .Length(6).WithMessage("OTP must be 6 digits")
                .Matches(@"^\d{6}$").WithMessage("OTP must contain only numbers");
        }
    }
}
