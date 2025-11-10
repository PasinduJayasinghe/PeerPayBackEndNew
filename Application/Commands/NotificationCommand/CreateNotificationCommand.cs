using Application.Dtos;
using Domain.Enums;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.NotificationCommand
{
    public class CreateNotificationCommand : IRequest<NotificationDto>
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public NotificationType Type { get; set; }
        public string ActionUrl { get; set; }
        public string Metadata { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }

    public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required")
                .MaximumLength(50).WithMessage("User ID cannot exceed 50 characters");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required")
                .MaximumLength(1000).WithMessage("Content cannot exceed 1000 characters");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid notification type");

            RuleFor(x => x.ActionUrl)
                .MaximumLength(500).WithMessage("Action URL cannot exceed 500 characters");

            RuleFor(x => x.ExpiresAt)
                .Must(date => !date.HasValue || date.Value > DateTime.UtcNow)
                .WithMessage("Expiry date must be in the future");
        }
    }
}
