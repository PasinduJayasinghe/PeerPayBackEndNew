using Application.Dtos;
using MediatR;

namespace Application.Queries.AdminQuery
{
    public class GetAllEmployersQuery : IRequest<List<EmployerAccountDto>>
    {
    }

    public class GetAllStudentsQuery : IRequest<List<StudentAccountDto>>
    {
    }

    public class GetAllJobsQuery : IRequest<List<JobDto>>
    {
    }

    public class GetAllApplicationsQuery : IRequest<List<JobApplicationDto>>
    {
    }
}
