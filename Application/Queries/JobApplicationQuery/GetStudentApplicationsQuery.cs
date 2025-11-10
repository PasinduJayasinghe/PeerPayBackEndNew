using Application.Dtos;
using MediatR;

namespace Application.Queries.JobApplicationQuery
{
    public class GetStudentApplicationsQuery : IRequest<List<JobApplicationDto>>
    {
        public string StudentId { get; set; } = string.Empty;
    }
}
