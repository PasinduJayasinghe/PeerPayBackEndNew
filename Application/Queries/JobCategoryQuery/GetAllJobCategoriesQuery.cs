using Application.Dtos;
using MediatR;

namespace Application.Queries.JobCategoryQuery
{
    public class GetAllJobCategoriesQuery : IRequest<IEnumerable<JobCategoryDto>>
    {
    }
}
