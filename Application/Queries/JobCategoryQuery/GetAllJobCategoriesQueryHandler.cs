using Application.Dtos;
using Application.Interfaces;
using MediatR;

namespace Application.Queries.JobCategoryQuery
{
    public class GetAllJobCategoriesQueryHandler : IRequestHandler<GetAllJobCategoriesQuery, IEnumerable<JobCategoryDto>>
    {
        private readonly IJobCategoryRepository _jobCategoryRepository;

        public GetAllJobCategoriesQueryHandler(IJobCategoryRepository jobCategoryRepository)
        {
            _jobCategoryRepository = jobCategoryRepository;
        }

        public async Task<IEnumerable<JobCategoryDto>> Handle(GetAllJobCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _jobCategoryRepository.GetAllAsync();
            
            return categories.Select(c => new JobCategoryDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                IsActive = c.IsActive,
                JobCount = c.Jobs?.Count ?? 0
            });
        }
    }
}
