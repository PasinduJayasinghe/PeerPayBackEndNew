using Application.Dtos;
using Domain.Enums;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.RatingCommand
{
    public class CreateRatingCommand : IRequest<RatingDto>
    {
        public string JobId { get; set; }
        public string RaterId { get; set; }
        public string RatedUserId { get; set; }
        public int RatingValue { get; set; }
        public string Review { get; set; }
        public RatingType RatingType { get; set; }
        public bool IsPublic { get; set; } = true;
    }

    public class CreateRatingCommandValidator : AbstractValidator<CreateRatingCommand>
    {
        public CreateRatingCommandValidator()
        {
            RuleFor(x => x.JobId)
                .NotEmpty().WithMessage("Job ID is required")
                .MaximumLength(50).WithMessage("Job ID cannot exceed 50 characters");

            RuleFor(x => x.RaterId)
                .NotEmpty().WithMessage("Rater ID is required")
                .MaximumLength(50).WithMessage("Rater ID cannot exceed 50 characters");

            RuleFor(x => x.RatedUserId)
                .NotEmpty().WithMessage("Rated user ID is required")
                .MaximumLength(50).WithMessage("Rated user ID cannot exceed 50 characters")
                .NotEqual(x => x.RaterId).WithMessage("Cannot rate yourself");

            RuleFor(x => x.RatingValue)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5");

            RuleFor(x => x.Review)
                .MaximumLength(1000).WithMessage("Review cannot exceed 1000 characters");

            RuleFor(x => x.RatingType)
                .IsInEnum().WithMessage("Invalid rating type");
        }
    }
}
