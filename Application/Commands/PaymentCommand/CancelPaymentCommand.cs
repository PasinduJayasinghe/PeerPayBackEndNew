using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.PaymentCommand
{
    public class CancelPaymentCommand : IRequest<bool>
    {
        public string PaymentId { get; set; }
    }

    public class CancelPaymentCommandValidator : AbstractValidator<CancelPaymentCommand>
    {
        public CancelPaymentCommandValidator()
        {
            RuleFor(x => x.PaymentId)
                .NotEmpty().WithMessage("Payment ID is required")
                .MaximumLength(50).WithMessage("Payment ID cannot exceed 50 characters");
        }
    }
}
