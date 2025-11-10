using Application.Interfaces;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Commands.JobApplicationCommand
{
    public class UpdateApplicationStatusCommandValidator : AbstractValidator<UpdateApplicationStatusCommand>
    {
        public UpdateApplicationStatusCommandValidator()
        {
            RuleFor(x => x.ApplicationId)
                .NotEmpty().WithMessage("Application ID is required");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required")
                .Must(BeValidStatus).WithMessage("Invalid status. Must be Pending, Accepted, or Rejected");

            RuleFor(x => x.UpdatedBy)
                .NotEmpty().WithMessage("UpdatedBy is required");
        }

        private bool BeValidStatus(string status)
        {
            return Enum.TryParse<ApplicationStatus>(status, true, out _);
        }
    }

    public class UpdateApplicationStatusCommandHandler : IRequestHandler<UpdateApplicationStatusCommand, bool>
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IEmployerRepository _employerRepository;

        public UpdateApplicationStatusCommandHandler(
            IJobApplicationRepository jobApplicationRepository,
            IEmployerRepository employerRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
            _employerRepository = employerRepository;
        }

        public async Task<bool> Handle(UpdateApplicationStatusCommand request, CancellationToken cancellationToken)
        {
            // Validate the command
            var validator = new UpdateApplicationStatusCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Get the application
            var application = await _jobApplicationRepository.GetApplicationByIdAsync(request.ApplicationId);
            if (application == null)
            {
                throw new Exception("Application not found");
            }

            // Verify the user is the employer who owns the job
            var employer = await _employerRepository.GetByUserIdAsync(request.UpdatedBy);
            if (employer == null)
            {
                throw new Exception("Employer not found");
            }

            if (application.Job.EmployerId != employer.EmployerId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this application");
            }

            // Parse the status
            if (!Enum.TryParse<ApplicationStatus>(request.Status, true, out var status))
            {
                throw new Exception("Invalid status");
            }

            // Update the application status
            var result = await _jobApplicationRepository.UpdateApplicationStatusAsync(
                request.ApplicationId, 
                status, 
                request.UpdatedBy,
                request.EmployerNotes);

            return result;
        }
    }
}
