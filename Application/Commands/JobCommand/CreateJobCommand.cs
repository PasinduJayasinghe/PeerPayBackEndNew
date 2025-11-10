using Application.Dtos;
using Domain.Enums;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.JobCommand
{
    public class CreateJobCommand : IRequest<JobDto>
    {
        public string EmployerId { get; set; }
        public string CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal PayAmount { get; set; }
        public PayType PayType { get; set; }
        public int DurationDays { get; set; }
        public string[] RequiredSkills { get; set; }
        public DateTime Deadline { get; set; }
        public string Location { get; set; }
        public JobType JobType { get; set; }
        public int MaxApplicants { get; set; }
    }

    public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
    {
        public CreateJobCommandValidator()
        {
            RuleFor(x => x.EmployerId)
                .NotEmpty().WithMessage("Employer ID is required")
                .MaximumLength(50).WithMessage("Employer ID cannot exceed 50 characters");

            RuleFor(x => x.CategoryId)
                .MaximumLength(50).WithMessage("Category ID cannot exceed 50 characters");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Job title is required")
                .MinimumLength(5).WithMessage("Title must be at least 5 characters")
                .MaximumLength(300).WithMessage("Title cannot exceed 300 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Job description is required")
                .MinimumLength(20).WithMessage("Description must be at least 20 characters")
                .MaximumLength(5000).WithMessage("Description cannot exceed 5000 characters");

            RuleFor(x => x.PayAmount)
                .GreaterThan(0).WithMessage("Pay amount must be greater than 0")
                .LessThanOrEqualTo(1000000).WithMessage("Pay amount cannot exceed 1,000,000");

            RuleFor(x => x.PayType)
                .IsInEnum().WithMessage("Invalid pay type");

            RuleFor(x => x.DurationDays)
                .GreaterThan(0).WithMessage("Duration must be at least 1 day")
                .LessThanOrEqualTo(365).WithMessage("Duration cannot exceed 365 days");

            RuleFor(x => x.RequiredSkills)
                .NotEmpty().WithMessage("At least one skill is required")
                .Must(skills => skills != null && skills.Length <= 20)
                .WithMessage("Cannot have more than 20 required skills");

            RuleFor(x => x.Deadline)
                .NotEmpty().WithMessage("Deadline is required")
                .GreaterThan(DateTime.UtcNow).WithMessage("Deadline must be in the future");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required")
                .MaximumLength(255).WithMessage("Location cannot exceed 255 characters");

            RuleFor(x => x.JobType)
                .IsInEnum().WithMessage("Invalid job type");

            RuleFor(x => x.MaxApplicants)
                .GreaterThan(0).WithMessage("Maximum applicants must be at least 1")
                .LessThanOrEqualTo(1000).WithMessage("Maximum applicants cannot exceed 1000");
        }
    }
}
