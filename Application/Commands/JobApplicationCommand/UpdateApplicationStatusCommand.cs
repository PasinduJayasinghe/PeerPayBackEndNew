using Application.Dtos;
using MediatR;

namespace Application.Commands.JobApplicationCommand
{
    public class UpdateApplicationStatusCommand : IRequest<bool>
    {
        public string ApplicationId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // "Accepted", "Rejected", "Pending"
        public string UpdatedBy { get; set; } = string.Empty; // Employer ID
        public string? EmployerNotes { get; set; }
    }
}
