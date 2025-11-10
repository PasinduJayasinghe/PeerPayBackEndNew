using Application.Dtos;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ConversationCommand
{
    public class CreateConversationCommand : IRequest<ConversationDto>
    {
        public string Participant1Id { get; set; }
        public string Participant2Id { get; set; }
        public string JobId { get; set; }
    }

    public class CreateConversationCommandValidator : AbstractValidator<CreateConversationCommand>
    {
        public CreateConversationCommandValidator()
        {
            RuleFor(x => x.Participant1Id)
                .NotEmpty().WithMessage("Participant 1 ID is required")
                .MaximumLength(50).WithMessage("Participant 1 ID cannot exceed 50 characters");

            RuleFor(x => x.Participant2Id)
                .NotEmpty().WithMessage("Participant 2 ID is required")
                .MaximumLength(50).WithMessage("Participant 2 ID cannot exceed 50 characters")
                .NotEqual(x => x.Participant1Id).WithMessage("Participants must be different users");

            RuleFor(x => x.JobId)
                .NotEmpty().WithMessage("Job ID is required")
                .MaximumLength(50).WithMessage("Job ID cannot exceed 50 characters");
        }
    }
}
