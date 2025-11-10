using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.MessageCommand
{
    public class MarkConversationAsReadCommand : IRequest<bool>
    {
        public string ConversationId { get; set; }
        public string UserId { get; set; }
    }

    public class MarkConversationAsReadCommandValidator : AbstractValidator<MarkConversationAsReadCommand>
    {
        public MarkConversationAsReadCommandValidator()
        {
            RuleFor(x => x.ConversationId)
                .NotEmpty().WithMessage("Conversation ID is required")
                .MaximumLength(50).WithMessage("Conversation ID cannot exceed 50 characters");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required")
                .MaximumLength(50).WithMessage("User ID cannot exceed 50 characters");
        }
    }
}
