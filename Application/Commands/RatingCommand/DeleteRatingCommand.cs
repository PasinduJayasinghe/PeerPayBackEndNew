using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.RatingCommand
{
    public class DeleteRatingCommand : IRequest<bool>
    {
        public string RatingId { get; set; } = string.Empty;
    }

    public class DeleteRatingCommandValidator : AbstractValidator<DeleteRatingCommand>
    {
        public DeleteRatingCommandValidator()
        {
            RuleFor(x => x.RatingId)
                .NotEmpty().WithMessage("Rating ID is required")
                .MaximumLength(50).WithMessage("Rating ID cannot exceed 50 characters");
        }
    }
}
