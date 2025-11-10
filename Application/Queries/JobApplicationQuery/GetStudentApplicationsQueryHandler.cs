using Application.Dtos;
using Application.Interfaces;
using MediatR;

namespace Application.Queries.JobApplicationQuery
{
    public class GetStudentApplicationsQueryHandler : IRequestHandler<GetStudentApplicationsQuery, List<JobApplicationDto>>
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public GetStudentApplicationsQueryHandler(IJobApplicationRepository jobApplicationRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
        }

        public async Task<List<JobApplicationDto>> Handle(GetStudentApplicationsQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.StudentId))
            {
                throw new ArgumentException("Student ID is required");
            }

            var applications = await _jobApplicationRepository.GetApplicationsByStudentIdAsync(request.StudentId);

            return applications.Select(app => new JobApplicationDto
            {
                Id = app.ApplicationId,
                JobId = app.JobId,
                JobTitle = app.Job?.Title ?? string.Empty,
                EmployerName = app.Job?.Employer?.User?.Name ?? string.Empty,
                StudentId = app.StudentId,
                StudentName = app.Student?.User?.Name ?? string.Empty,
                AppliedAt = app.AppliedDate,
                Status = app.Status.ToString(),
                CoverLetter = app.CoverLetter,
                Attachments = app.Attachments,
                StatusUpdatedAt = app.StatusUpdatedAt,
                EmployerNotes = app.EmployerNotes
            }).ToList();
        }
    }
}
