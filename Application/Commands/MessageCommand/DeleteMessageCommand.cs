using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.MessageCommand
{
    public class DeleteMessageCommand : IRequest<bool>
    {
        public string MessageId { get; set; } = string.Empty;
    }

    public class DeleteMessageCommandValidator : AbstractValidator<DeleteMessageCommand>
    {
        public DeleteMessageCommandValidator()
        {
            RuleFor(x => x.MessageId)
                .NotEmpty().WithMessage("Message ID is required")
                .MaximumLength(50).WithMessage("Message ID cannot exceed 50 characters");
        }
    }
}
