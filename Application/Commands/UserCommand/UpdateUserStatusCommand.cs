using Domain.Enums;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserCommand
{
    public class UpdateUserStatusCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public UserStatus Status { get; set; }
    }

    public class UpdateUserStatusCommandValidator : AbstractValidator<UpdateUserStatusCommand>
    {
        public UpdateUserStatusCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required")
                .MaximumLength(50).WithMessage("User ID cannot exceed 50 characters");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid user status");
        }
    }
}
