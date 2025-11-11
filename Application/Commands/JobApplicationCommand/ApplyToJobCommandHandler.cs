using Application.Dtos;
using Application.Interfaces;
using Domain.Classes;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Commands.JobApplicationCommand
{
    public class ApplyToJobCommandValidator : AbstractValidator<ApplyToJobCommand>
    {
        public ApplyToJobCommandValidator()
        {
            RuleFor(x => x.JobId)
                .NotEmpty().WithMessage("Job ID is required");

            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("Student ID is required");

            RuleFor(x => x.CoverLetter)
                .NotEmpty().WithMessage("Cover letter is required")
                .MaximumLength(2000).WithMessage("Cover letter must not exceed 2000 characters");
        }
    }

    public class ApplyToJobCommandHandler : IRequestHandler<ApplyToJobCommand, JobApplicationDto>
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IStudentRepository _studentRepository;

        public ApplyToJobCommandHandler(
            IJobApplicationRepository jobApplicationRepository,
            IJobRepository jobRepository,
            IStudentRepository studentRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
            _jobRepository = jobRepository;
            _studentRepository = studentRepository;
        }

        public async Task<JobApplicationDto> Handle(ApplyToJobCommand request, CancellationToken cancellationToken)
        {
            // Validate the command
            var validator = new ApplyToJobCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Check if job exists and is active
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job == null)
            {
                throw new Exception("Job not found");
            }

            if (job.Status != JobStatus.Active)
            {
                throw new Exception("This job is no longer accepting applications");
            }

            // Check if deadline has passed
            if (job.Deadline < DateTime.UtcNow)
            {
                throw new Exception("The application deadline has passed");
            }

            // Check if student exists
            var student = await _studentRepository.GetStudentByIdAsync(request.StudentId);
            if (student == null)
            {
                throw new Exception("Student not found");
            }

            // Check if student has already applied
            var existingApplication = await _jobApplicationRepository.HasStudentAppliedToJobAsync(request.StudentId, request.JobId);
            if (existingApplication)
            {
                throw new Exception("You have already applied to this job");
            }

            // Check if max applicants reached
            if (job.MaxApplicants > 0)
            {
                var applicationCount = await _jobApplicationRepository.GetApplicationCountByJobIdAsync(request.JobId);
                if (applicationCount >= job.MaxApplicants)
                {
                    throw new Exception("This job has reached the maximum number of applicants");
                }
            }

            // Create application
            var application = new JobApplication
            {
                ApplicationId = Guid.NewGuid().ToString(),
                JobId = request.JobId,
                StudentId = request.StudentId,
                AppliedDate = DateTime.UtcNow,
                Status = ApplicationStatus.Submitted,
                CoverLetter = request.CoverLetter,
                Attachments = Array.Empty<string>(), // No attachments for now
                StatusUpdatedAt = DateTime.UtcNow,
                UpdatedBy = request.StudentId,
                EmployerNotes = string.Empty
            };

            try
            {
                var createdApplication = await _jobApplicationRepository.CreateApplicationAsync(application);

                // Load the full application with related data for the response
                var fullApplication = await _jobApplicationRepository.GetApplicationByIdAsync(createdApplication.ApplicationId);

                if (fullApplication == null)
                {
                    throw new Exception("Failed to retrieve created application");
                }

                return new JobApplicationDto
                {
                    Id = fullApplication.ApplicationId,
                    JobId = fullApplication.JobId,
                    JobTitle = fullApplication.Job?.Title ?? string.Empty,
                    StudentId = fullApplication.StudentId,
                    StudentName = fullApplication.Student?.User?.Name ?? string.Empty,
                    AppliedAt = fullApplication.AppliedDate,
                    Status = fullApplication.Status.ToString(),
                    CoverLetter = fullApplication.CoverLetter,
                    Attachments = fullApplication.Attachments ?? Array.Empty<string>(),
                    StatusUpdatedAt = fullApplication.StatusUpdatedAt,
                    EmployerNotes = fullApplication.EmployerNotes
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving the entity changes. See the inner exception for details.", ex);
            }
        }
    }
}