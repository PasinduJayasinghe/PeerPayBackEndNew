using System;

namespace Application.Dtos
{
    public class EmployerDto
    {
        public string EmployerId { get; set; }
        public string UserId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public string Description { get; set; }
        public string ContactPerson { get; set; }
        public string VerificationStatus { get; set; }
        public decimal? Rating { get; set; }
        public int JobsPosted { get; set; }
        public UserDto User { get; set; }
    }
}
