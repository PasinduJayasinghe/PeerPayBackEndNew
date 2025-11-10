using Application.Dtos;
using MediatR;

namespace Application.Commands.JobApplicationCommand
{
    public class ApplyToJobCommand : IRequest<JobApplicationDto>
    {
        public string JobId { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public string CoverLetter { get; set; } = string.Empty;
        public string[]? Attachments { get; set; }
    }
}
