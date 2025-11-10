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
    public class ConfirmPaymentCommand : IRequest<PaymentDto>
    {
        public string PaymentId { get; set; }
        public string PaymentIntentId { get; set; }
    }

    public class ConfirmPaymentCommandValidator : AbstractValidator<ConfirmPaymentCommand>
    {
        public ConfirmPaymentCommandValidator()
        {
            RuleFor(x => x.PaymentId)
                .NotEmpty().WithMessage("Payment ID is required")
                .MaximumLength(50).WithMessage("Payment ID cannot exceed 50 characters");

            RuleFor(x => x.PaymentIntentId)
                .NotEmpty().WithMessage("Payment Intent ID is required");
        }
    }
}
