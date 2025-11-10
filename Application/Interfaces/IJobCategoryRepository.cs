using Domain.Classes;

namespace Application.Interfaces
{
    public interface IJobCategoryRepository
    {
        Task<IEnumerable<JobCategory>> GetAllAsync();
        Task<JobCategory> GetByIdAsync(string categoryId);
        Task<JobCategory> AddAsync(JobCategory category);
        Task<JobCategory> UpdateAsync(JobCategory category);
        Task<bool> DeleteAsync(string categoryId);
    }
}
