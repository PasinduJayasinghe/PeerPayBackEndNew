using Application.Interfaces;
using Domain.Classes;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class JobCategoryRepository : IJobCategoryRepository
    {
        private readonly PeerPayDbContext _context;

        public JobCategoryRepository(PeerPayDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobCategory>> GetAllAsync()
        {
            return await _context.JobCategories
                .Include(c => c.Jobs)
                .Where(c => c.IsActive)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<JobCategory> GetByIdAsync(string categoryId)
        {
            return await _context.JobCategories
                .Include(c => c.Jobs)
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        public async Task<JobCategory> AddAsync(JobCategory category)
        {
            _context.JobCategories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<JobCategory> UpdateAsync(JobCategory category)
        {
            _context.JobCategories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(string categoryId)
        {
            var category = await GetByIdAsync(categoryId);
            if (category == null)
                return false;

            _context.JobCategories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
