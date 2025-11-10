using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.NotificationCommand
{
    public class MarkAllNotificationsAsReadCommand : IRequest<bool>
    {
        public string UserId { get; set; }
    }

    public class MarkAllNotificationsAsReadCommandValidator : AbstractValidator<MarkAllNotificationsAsReadCommand>
    {
        public MarkAllNotificationsAsReadCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required")
                .MaximumLength(50).WithMessage("User ID cannot exceed 50 characters");
        }
    }
}
