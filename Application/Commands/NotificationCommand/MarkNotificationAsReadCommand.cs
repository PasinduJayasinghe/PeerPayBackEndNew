using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.NotificationCommand
{
    public class MarkNotificationAsReadCommand : IRequest<bool>
    {
        public string NotificationId { get; set; } = string.Empty;
    }

    public class MarkNotificationAsReadCommandValidator : AbstractValidator<MarkNotificationAsReadCommand>
    {
        public MarkNotificationAsReadCommandValidator()
        {
            RuleFor(x => x.NotificationId)
                .NotEmpty().WithMessage("Notification ID is required")
                .MaximumLength(50).WithMessage("Notification ID cannot exceed 50 characters");
        }
    }
}
