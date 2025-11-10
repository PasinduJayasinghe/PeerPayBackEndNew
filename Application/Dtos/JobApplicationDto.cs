namespace Application.Dtos
{
    public class JobApplicationDto
    {
        public string Id { get; set; } = string.Empty;
        public string JobId { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string? EmployerName { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string? StudentEmail { get; set; }
        public string? StudentPhone { get; set; }
        public string? University { get; set; }
        public string? Course { get; set; }
        public int? YearOfStudy { get; set; }
        public DateTime AppliedAt { get; set; }
        public string Status { get; set; } = string.Empty;
        public string CoverLetter { get; set; } = string.Empty;
        public string[] Attachments { get; set; } = Array.Empty<string>();
        public DateTime StatusUpdatedAt { get; set; }
        public string? EmployerNotes { get; set; }
    }
}
