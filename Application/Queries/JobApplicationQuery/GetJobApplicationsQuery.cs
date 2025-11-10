using Application.Dtos;
using MediatR;

namespace Application.Queries.JobApplicationQuery
{
    public class GetJobApplicationsQuery : IRequest<List<JobApplicationDto>>
    {
        public string JobId { get; set; } = string.Empty;
    }
}
