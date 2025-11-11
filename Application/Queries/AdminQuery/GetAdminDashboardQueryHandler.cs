using Application.Dtos;
using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Queries.AdminQuery
{
    public class GetAdminDashboardQueryHandler : IRequestHandler<GetAdminDashboardQuery, AdminDashboardDto>
    {
        private readonly IEmployerRepository _employerRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IUserRepository _userRepository;

        public GetAdminDashboardQueryHandler(
            IEmployerRepository employerRepository,
            IStudentRepository studentRepository,
            IJobRepository jobRepository,
            IJobApplicationRepository jobApplicationRepository,
            IUserRepository userRepository)
        {
            _employerRepository = employerRepository;
            _studentRepository = studentRepository;
            _jobRepository = jobRepository;
            _jobApplicationRepository = jobApplicationRepository;
            _userRepository = userRepository;
        }

        public async Task<AdminDashboardDto> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
        {
            var dashboard = new AdminDashboardDto();

            // Get all data
            var allEmployers = await _employerRepository.GetAllAsync();
            var allStudents = await _studentRepository.GetAllAsync();
            var allJobs = await _jobRepository.GetAllAsync();
            var allApplications = await _jobApplicationRepository.GetAllAsync();

            // Calculate statistics
            dashboard.Statistics = new DashboardStatistics
            {
                TotalEmployers = allEmployers.Count(),
                TotalStudents = allStudents.Count(),
                TotalJobs = allJobs.Count(),
                ActiveJobs = allJobs.Count(j => j.Status == JobStatus.Active),
                TotalApplications = allApplications.Count(),
                PendingApplications = allApplications.Count(a => 
                    a.Status == ApplicationStatus.Submitted || 
                    a.Status == ApplicationStatus.UnderReview),
                VerifiedEmployers = allEmployers.Count(e => e.VerificationStatus == "Verified"),
                VerifiedStudents = allStudents.Count(s => s.AcademicVerificationStatus == "Verified")
            };

            // Get recent employers (last 10)
            var recentEmployers = allEmployers
                .OrderByDescending(e => e.User?.CreatedAt ?? DateTime.MinValue)
                .Take(request.RecentItemsCount)
                .ToList();

            dashboard.RecentEmployers = recentEmployers.Select(e => new EmployerAccountDto
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
            }).ToList();

            // Get recent students (last 10)
            var recentStudents = allStudents
                .OrderByDescending(s => s.User?.CreatedAt ?? DateTime.MinValue)
                .Take(request.RecentItemsCount)
                .ToList();

            dashboard.RecentStudents = recentStudents.Select(s => new StudentAccountDto
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
            }).ToList();

            // Get recent jobs (last 10)
            var recentJobs = allJobs
                .OrderByDescending(j => j.PostedDate)
                .Take(request.RecentItemsCount)
                .ToList();

            dashboard.RecentJobs = recentJobs.Select(j => new JobDto
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
            }).ToList();

            // Get recent applications (last 10)
            var recentApplications = allApplications
                .OrderByDescending(a => a.AppliedDate)
                .Take(request.RecentItemsCount)
                .ToList();

            // Fetch student details for applications
            var userIds = recentApplications.Select(a => a.UserId).Distinct().ToList();
            var students = new List<Domain.Classes.Student>();
            foreach (var userId in userIds)
            {
                var student = await _studentRepository.GetByUserIdAsync(userId);
                if (student != null)
                {
                    students.Add(student);
                }
            }

            dashboard.RecentApplications = recentApplications.Select(a =>
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
                    StatusUpdatedAt = a.StatusUpdatedAt
                };
            }).ToList();

            return dashboard;
        }
    }
}
