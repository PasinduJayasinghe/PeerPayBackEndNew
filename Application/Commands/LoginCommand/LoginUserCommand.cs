using Application.Dtos;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.LoginCommand
{
    public class LoginUserCommand : IRequest<LoginResponseDto>
    {
        public string EmailOrPhone { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.EmailOrPhone)
                .NotEmpty().WithMessage("Email or phone number is required")
                .Must(BeValidEmailOrPhone).WithMessage("Must be a valid email or phone number");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }

        private bool BeValidEmailOrPhone(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            // Check if it's a valid email
            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (System.Text.RegularExpressions.Regex.IsMatch(value, emailRegex))
                return true;

            // Check if it's a valid Sri Lankan phone number
            var phoneRegex = @"^(\+94[0-9]{9}|0[0-9]{9})$";
            if (System.Text.RegularExpressions.Regex.IsMatch(value, phoneRegex))
                return true;

            return false;
        }
    }
}
