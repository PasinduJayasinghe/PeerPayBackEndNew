using Application.Dtos;
using FluentValidation;
using MediatR;

namespace Application.Commands.OTPCommand
{
    public class SendOtpCommand : IRequest<OtpResponseDto>
    {
        public string Email { get; set; }
    }

    public class SendOtpCommandValidator : AbstractValidator<SendOtpCommand>
    {
        public SendOtpCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
        }
    }
}
