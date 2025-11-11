using Application.Dtos;
using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Queries.AdminQuery
{
    // Get All Employers Handler
    public class GetAllEmployersQueryHandler : IRequestHandler<GetAllEmployersQuery, List<EmployerAccountDto>>
    {
        private readonly IEmployerRepository _employerRepository;

        public GetAllEmployersQueryHandler(IEmployerRepository employerRepository)
        {
            _employerRepository = employerRepository;
        }

        public async Task<List<EmployerAccountDto>> Handle(GetAllEmployersQuery request, CancellationToken cancellationToken)
        {
            var employers = await _employerRepository.GetAllAsync();

            return employers.Select(e => new EmployerAccountDto
            {
                EmployerId = e.EmployerId,
                UserId = e.UserId,
                Name = e.User?.Name ?? string.Empty,
                Email = e.User?.Email ?? string.Empty,
                CompanyName = e.CompanyName,
                CompanyType = e.CompanyType,
                VerificationStatus = e.VerificationStatus,
                Rating = e.Rating,
                JobsPosted = e.JobsPosted,
                CreatedAt = e.User?.CreatedAt ?? DateTime.MinValue,
                Status = e.User?.Status.ToString() ?? string.Empty
            }).OrderByDescending(e => e.CreatedAt).ToList();
        }
    }

    // Get All Students Handler
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, List<StudentAccountDto>>
    {
        private readonly IStudentRepository _studentRepository;

        public GetAllStudentsQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<StudentAccountDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetAllAsync();

            return students.Select(s => new StudentAccountDto
            {
                StudentId = s.StudentId,
                UserId = s.UserId,
                Name = s.User?.Name ?? string.Empty,
                Email = s.User?.Email ?? string.Empty,
                University = s.University,
                Course = s.Course,
                YearOfStudy = s.YearOfStudy,
                AcademicVerificationStatus = s.AcademicVerificationStatus,
                Rating = s.Rating,
                CompletedJobs = s.CompletedJobs,
                TotalEarnings = s.TotalEarnings,
                CreatedAt = s.User?.CreatedAt ?? DateTime.MinValue,
                Status = s.User?.Status.ToString() ?? string.Empty
            }).OrderByDescending(s => s.CreatedAt).ToList();
        }
    }

    // Get All Jobs Handler
    public class GetAllJobsQueryHandler : IRequestHandler<GetAllJobsQuery, List<JobDto>>
    {
        private readonly IJobRepository _jobRepository;

        public GetAllJobsQueryHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<List<JobDto>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetAllAsync();

            return jobs.Select(j => new JobDto
            {
                JobId = j.JobId,
                Title = j.Title,
                Description = j.Description,
                EmployerId = j.EmployerId,
                EmployerName = j.Employer?.CompanyName ?? string.Empty,
                CategoryId = j.CategoryId ?? string.Empty,
                CategoryName = j.Category?.Name ?? string.Empty,
                PayAmount = j.PayAmount,
                PayType = j.PayType,
                Location = j.Location,
                JobType = j.JobType,
                Status = j.Status,
                PostedDate = j.PostedDate,
                Deadline = j.Deadline,
                RequiredSkills = j.RequiredSkills ?? Array.Empty<string>(),
                ApplicationCount = j.Applications?.Count ?? 0
            }).OrderByDescending(j => j.PostedDate).ToList();
        }
    }

    // Get All Applications Handler
    public class GetAllApplicationsQueryHandler : IRequestHandler<GetAllApplicationsQuery, List<JobApplicationDto>>
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IStudentRepository _studentRepository;

        public GetAllApplicationsQueryHandler(
            IJobApplicationRepository jobApplicationRepository,
            IStudentRepository studentRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
            _studentRepository = studentRepository;
        }

        public async Task<List<JobApplicationDto>> Handle(GetAllApplicationsQuery request, CancellationToken cancellationToken)
        {
            var applications = await _jobApplicationRepository.GetAllAsync();

            // Get all unique user IDs
            var userIds = applications.Select(a => a.UserId).Distinct().ToList();
            
            // Fetch all students
            var students = new List<Domain.Classes.Student>();
            foreach (var userId in userIds)
            {
                var student = await _studentRepository.GetByUserIdAsync(userId);
                if (student != null)
                {
                    students.Add(student);
                }
            }

            return applications.Select(a =>
            {
                var student = students.FirstOrDefault(s => s.UserId == a.UserId);
                
                return new JobApplicationDto
                {
                    Id = a.ApplicationId,
                    JobId = a.JobId,
                    JobTitle = a.Job?.Title ?? string.Empty,
                    EmployerName = a.Job?.Employer?.CompanyName ?? string.Empty,
                    StudentId = a.UserId,
                    StudentName = a.User?.Name ?? string.Empty,
                    StudentEmail = a.User?.Email ?? string.Empty,
                    University = student?.University ?? string.Empty,
                    Course = student?.Course ?? string.Empty,
                    AppliedAt = a.AppliedDate,
                    Status = a.Status.ToString(),
                    CoverLetter = a.CoverLetter,
                    StatusUpdatedAt = a.StatusUpdatedAt,
                    EmployerNotes = a.EmployerNotes
                };
            }).OrderByDescending(a => a.AppliedAt).ToList();
        }
    }
}
