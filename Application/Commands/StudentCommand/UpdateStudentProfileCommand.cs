using Application.Dtos;
using FluentValidation;
using MediatR;

namespace Application.Commands.StudentCommand
{
    public class UpdateStudentProfileCommand : IRequest<StudentProfileDto>
    {
        public string StudentId { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? University { get; set; }
        public string? Course { get; set; }
        public int? YearOfStudy { get; set; }
        public string? Bio { get; set; }
        public string? Address { get; set; }
        public string? CvUrl { get; set; }
    }

    public class UpdateStudentProfileCommandValidator : AbstractValidator<UpdateStudentProfileCommand>
    {
        public UpdateStudentProfileCommandValidator()
        {
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("Student ID is required");

            When(x => !string.IsNullOrEmpty(x.Name), () =>
            {
                RuleFor(x => x.Name)
                    .MinimumLength(2).WithMessage("Name must be at least 2 characters")
                    .MaximumLength(200).WithMessage("Name cannot exceed 200 characters");
            });

            When(x => !string.IsNullOrEmpty(x.Phone), () =>
            {
                RuleFor(x => x.Phone)
                    .Matches(@"^(\+94[0-9]{9}|0[0-9]{9})$")
                    .WithMessage("Phone number must be a valid Sri Lankan number");
            });

            When(x => !string.IsNullOrEmpty(x.University), () =>
            {
                RuleFor(x => x.University)
                    .MaximumLength(200).WithMessage("University name cannot exceed 200 characters");
            });

            When(x => !string.IsNullOrEmpty(x.Course), () =>
            {
                RuleFor(x => x.Course)
                    .MaximumLength(200).WithMessage("Course name cannot exceed 200 characters");
            });

            When(x => x.YearOfStudy.HasValue, () =>
            {
                RuleFor(x => x.YearOfStudy)
                    .InclusiveBetween(1, 7).WithMessage("Year of study must be between 1 and 7");
            });

            When(x => !string.IsNullOrEmpty(x.Bio), () =>
            {
                RuleFor(x => x.Bio)
                    .MaximumLength(1000).WithMessage("Bio cannot exceed 1000 characters");
            });
        }
    }
}
