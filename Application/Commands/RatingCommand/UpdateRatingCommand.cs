using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.RatingCommand
{
    public class UpdateRatingCommand : IRequest<bool>
    {
        public string RatingId { get; set; }
        public int RatingValue { get; set; }
        public string Review { get; set; }
        public bool IsPublic { get; set; }
    }

    public class UpdateRatingCommandValidator : AbstractValidator<UpdateRatingCommand>
    {
        public UpdateRatingCommandValidator()
        {
            RuleFor(x => x.RatingId)
                .NotEmpty().WithMessage("Rating ID is required")
                .MaximumLength(50).WithMessage("Rating ID cannot exceed 50 characters");

            RuleFor(x => x.RatingValue)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5");

            RuleFor(x => x.Review)
                .MaximumLength(1000).WithMessage("Review cannot exceed 1000 characters");
        }
    }
}
