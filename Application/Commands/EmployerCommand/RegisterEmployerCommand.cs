using Application.Dtos;
using FluentValidation;
using MediatR;

namespace Application.Commands.EmployerCommand
{
    public class RegisterEmployerCommand : IRequest<UserResponseDto>
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public string Description { get; set; }
        public string ContactPerson { get; set; }
    }

    public class RegisterEmployerCommandValidator : AbstractValidator<RegisterEmployerCommand>
    {
        public RegisterEmployerCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(255).WithMessage("Email cannot exceed 255 characters");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^(\+94[0-9]{9}|0[0-9]{9})$")
                .WithMessage("Phone number must be a valid Sri Lankan number");

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
                .Matches(@"[@$!%*?&#]").WithMessage("Password must contain at least one special character");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required")
                .Equal(x => x.Password).WithMessage("Passwords do not match");

            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("Company name is required")
                .MaximumLength(255).WithMessage("Company name cannot exceed 255 characters");

            RuleFor(x => x.CompanyType)
                .NotEmpty().WithMessage("Company type is required")
                .MaximumLength(100).WithMessage("Company type cannot exceed 100 characters");

            RuleFor(x => x.ContactPerson)
                .NotEmpty().WithMessage("Contact person is required")
                .MaximumLength(200).WithMessage("Contact person name cannot exceed 200 characters");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");
        }
    }
}
