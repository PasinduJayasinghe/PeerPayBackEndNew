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
    public class RefundPaymentCommand : IRequest<bool>
    {
        public string PaymentId { get; set; }
        public decimal? RefundAmount { get; set; }
        public string Reason { get; set; }
    }

    public class RefundPaymentCommandValidator : AbstractValidator<RefundPaymentCommand>
    {
        public RefundPaymentCommandValidator()
        {
            RuleFor(x => x.PaymentId)
                .NotEmpty().WithMessage("Payment ID is required")
                .MaximumLength(50).WithMessage("Payment ID cannot exceed 50 characters");

            RuleFor(x => x.RefundAmount)
                .GreaterThan(0).When(x => x.RefundAmount.HasValue)
                .WithMessage("Refund amount must be greater than 0");

            RuleFor(x => x.Reason)
                .MaximumLength(500).WithMessage("Reason cannot exceed 500 characters");
        }
    }
}
