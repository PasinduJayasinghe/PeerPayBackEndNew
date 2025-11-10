namespace Application.Dtos
{
    public class StudentProfileDto
    {
        public string StudentId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string University { get; set; }
        public string Course { get; set; }
        public int YearOfStudy { get; set; }
        public string AcademicVerificationStatus { get; set; }
        public decimal Rating { get; set; }
        public int CompletedJobs { get; set; }
        public decimal TotalEarnings { get; set; }
        public string CvUrl { get; set; }
        public string Bio { get; set; }
        public string Address { get; set; }
        public string ProfilePictureUrl { get; set; }
    }

    public class UpdateStudentProfileDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string University { get; set; }
        public string Course { get; set; }
        public int? YearOfStudy { get; set; }
        public string Bio { get; set; }
        public string Address { get; set; }
        public string CvUrl { get; set; }
    }
}
