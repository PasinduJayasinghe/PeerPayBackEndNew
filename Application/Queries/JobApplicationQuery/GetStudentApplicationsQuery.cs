using Application.Dtos;
using MediatR;

namespace Application.Queries.JobApplicationQuery
{
    public class GetStudentApplicationsQuery : IRequest<List<JobApplicationDto>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
