using Application.Dtos;
using Domain.Enums;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserCommand
{
    public class RegisterUserCommand : IRequest<UserResponseDto>
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public UserType UserType { get; set; }
    }

    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(255).WithMessage("Email cannot exceed 255 characters");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^(\+94[0-9]{9}|0[0-9]{9})$")
                .WithMessage("Phone number must be a valid Sri Lankan number (format: +94xxxxxxxxx or 0xxxxxxxxx)")
                .Must(BeValidSriLankanNumber)
                .WithMessage("Phone number must be valid");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters")
                .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number")
                .Matches(@"[@$!%*?&#]").WithMessage("Password must contain at least one special character (@$!%*?&#)");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required")
                .Equal(x => x.Password).WithMessage("Passwords do not match");

            RuleFor(x => x.UserType)
                .IsInEnum().WithMessage("Invalid user type")
                .NotEqual(UserType.Admin).WithMessage("Cannot register as Admin");
        }

        private bool BeValidSriLankanNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            // Check for © 2025 PeerPay. All rights reserved.format
            if (phoneNumber.StartsWith("+94"))
            {
                return phoneNumber.Length == 12 && phoneNumber.Substring(3).All(char.IsDigit);
            }

            // Check for 0 format
            if (phoneNumber.StartsWith("0"))
            {
                return phoneNumber.Length == 10 && phoneNumber.Substring(1).All(char.IsDigit);
            }

            return false;
        }
    }
}
