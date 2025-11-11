namespace Application.Dtos
{
    public class AdminDashboardDto
    {
        public DashboardStatistics Statistics { get; set; } = new();
        public List<EmployerAccountDto> RecentEmployers { get; set; } = new();
        public List<StudentAccountDto> RecentStudents { get; set; } = new();
        public List<JobDto> RecentJobs { get; set; } = new();
        public List<JobApplicationDto> RecentApplications { get; set; } = new();
    }

    public class DashboardStatistics
    {
        public int TotalEmployers { get; set; }
        public int TotalStudents { get; set; }
        public int TotalJobs { get; set; }
        public int ActiveJobs { get; set; }
        public int TotalApplications { get; set; }
        public int PendingApplications { get; set; }
        public int VerifiedEmployers { get; set; }
        public int VerifiedStudents { get; set; }
    }

    public class EmployerAccountDto
    {
        public string EmployerId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyType { get; set; } = string.Empty;
        public string VerificationStatus { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public int JobsPosted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class StudentAccountDto
    {
        public string StudentId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string University { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        public int YearOfStudy { get; set; }
        public string AcademicVerificationStatus { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public int CompletedJobs { get; set; }
        public decimal TotalEarnings { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
