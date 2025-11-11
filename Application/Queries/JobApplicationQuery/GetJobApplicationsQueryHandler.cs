using Application.Dtos;
using Application.Interfaces;
using MediatR;

namespace Application.Queries.JobApplicationQuery
{
    public class GetJobApplicationsQueryHandler : IRequestHandler<GetJobApplicationsQuery, List<JobApplicationDto>>
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public GetJobApplicationsQueryHandler(IJobApplicationRepository jobApplicationRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
        }

        public async Task<List<JobApplicationDto>> Handle(GetJobApplicationsQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.JobId))
            {
                throw new ArgumentException("Job ID is required");
            }

            var applications = await _jobApplicationRepository.GetApplicationsByJobIdAsync(request.JobId);

            return applications.Select(app => new JobApplicationDto
            {
                Id = app.ApplicationId,
                JobId = app.JobId,
                JobTitle = app.Job?.Title ?? string.Empty,
                StudentId = app.StudentId,
                StudentName = app.Student?.User?.Name ?? string.Empty,
                StudentEmail = app.Student?.User?.Email ?? string.Empty,
                StudentPhone = app.Student?.User?.Phone ?? string.Empty,
                University = app.Student?.University ?? string.Empty,
                Course = app.Student?.Course ?? string.Empty,
                YearOfStudy = app.Student?.YearOfStudy ?? 0,
                AppliedAt = app.AppliedDate,
                Status = app.Status.ToString(),
                CoverLetter = app.CoverLetter,
                Attachments = app.Attachments ?? Array.Empty<string>(),
                StatusUpdatedAt = app.StatusUpdatedAt,
                EmployerNotes = app.EmployerNotes
            }).ToList();
        }
    }
}
