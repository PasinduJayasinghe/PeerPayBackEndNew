namespace Application.Dtos
{
    /// <summary>
    /// DTO for job application from frontend
    /// Maps studentId (from frontend) to UserId (backend)
    /// </summary>
    public class ApplyToJobDto
    {
        public string JobId { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public string CoverLetter { get; set; } = string.Empty;
    }
}
