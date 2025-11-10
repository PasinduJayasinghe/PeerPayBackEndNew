namespace Application.Dtos
{
    public class OtpResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
