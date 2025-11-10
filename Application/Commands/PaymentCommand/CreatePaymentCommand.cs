using Application.Dtos;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.PaymentCommand
{
    public class CreatePaymentCommand : IRequest<PaymentIntentDto>
    {
        public string JobId { get; set; }
        public string EmployerId { get; set; }
        public string StudentId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "usd";
        public string Notes { get; set; }
    }

    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.JobId)
                .NotEmpty().WithMessage("Job ID is required")
                .MaximumLength(50).WithMessage("Job ID cannot exceed 50 characters");

            RuleFor(x => x.EmployerId)
                .NotEmpty().WithMessage("Employer ID is required")
                .MaximumLength(50).WithMessage("Employer ID cannot exceed 50 characters");

            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("Student ID is required")
                .MaximumLength(50).WithMessage("Student ID cannot exceed 50 characters");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0")
                .LessThanOrEqualTo(100000).WithMessage("Amount cannot exceed 100,000");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required")
                .Length(3).WithMessage("Currency must be 3 characters (e.g., USD, EUR)");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notes cannot exceed 500 characters");
        }
    }
}
