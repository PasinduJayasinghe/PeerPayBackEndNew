using Application.Dtos;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.MessageCommand
{
    public class SendMessageCommand : IRequest<MessageDto>
    {
        public string ConversationId { get; set; }
        public string SenderId { get; set; }
        public string Content { get; set; }
        public string[] Attachments { get; set; }
    }

    public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
    {
        public SendMessageCommandValidator()
        {
            RuleFor(x => x.ConversationId)
                .NotEmpty().WithMessage("Conversation ID is required")
                .MaximumLength(50).WithMessage("Conversation ID cannot exceed 50 characters");

            RuleFor(x => x.SenderId)
                .NotEmpty().WithMessage("Sender ID is required")
                .MaximumLength(50).WithMessage("Sender ID cannot exceed 50 characters");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Message content is required")
                .MaximumLength(2000).WithMessage("Message content cannot exceed 2000 characters");

            RuleFor(x => x.Attachments)
                .Must(a => a == null || a.Length <= 5).WithMessage("Cannot attach more than 5 files");
        }
    }
}
