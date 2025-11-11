using Application.Dtos;
using Application.Interfaces;
using MediatR;

namespace Application.Queries.JobApplicationQuery
{
    public class GetJobApplicationsQueryHandler : IRequestHandler<GetJobApplicationsQuery, List<JobApplicationDto>>
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IStudentRepository _studentRepository;

        public GetJobApplicationsQueryHandler(
            IJobApplicationRepository jobApplicationRepository,
            IStudentRepository studentRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
            _studentRepository = studentRepository;
        }

        public async Task<List<JobApplicationDto>> Handle(GetJobApplicationsQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.JobId))
            {
                throw new ArgumentException("Job ID is required");
            }

            var applications = await _jobApplicationRepository.GetApplicationsByJobIdAsync(request.JobId);

            // Get all unique user IDs from applications
            var userIds = applications.Select(a => a.UserId).Distinct().ToList();
            
            // Fetch all students for these users
            var students = new List<Domain.Classes.Student>();
            foreach (var userId in userIds)
            {
                var student = await _studentRepository.GetByUserIdAsync(userId);
                if (student != null)
                {
                    students.Add(student);
                }
            }

            return applications.Select(app =>
            {
                var student = students.FirstOrDefault(s => s.UserId == app.UserId);
                
                return new JobApplicationDto
                {
                    Id = app.ApplicationId,
                    JobId = app.JobId,
                    JobTitle = app.Job?.Title ?? string.Empty,
                    StudentId = app.UserId,
                    StudentName = app.User?.Name ?? string.Empty,
                    StudentEmail = app.User?.Email ?? string.Empty,
                    StudentPhone = app.User?.Phone ?? string.Empty,
                    University = student?.University ?? string.Empty,
                    Course = student?.Course ?? string.Empty,
                    YearOfStudy = student?.YearOfStudy ?? 0,
                    AppliedAt = app.AppliedDate,
                    Status = app.Status.ToString(),
                    CoverLetter = app.CoverLetter,
                    Attachments = app.Attachments ?? Array.Empty<string>(),
                    StatusUpdatedAt = app.StatusUpdatedAt,
                    EmployerNotes = app.EmployerNotes
                };
            }).ToList();
        }
    }
}
