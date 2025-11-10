using Application.Interfaces;
using Domain.Classes;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly PeerPayDbContext _context;

        public JobRepository(PeerPayDbContext context)
        {
            _context = context;
        }

        public async Task<Job> GetByIdAsync(string jobId)
        {
            return await _context.Jobs
                .Include(j => j.Employer)
                    .ThenInclude(e => e.User)
                .Include(j => j.Category)
                .Include(j => j.Applications)
                    .ThenInclude(a => a.Student)
                        .ThenInclude(s => s.User)
                .Include(j => j.Ratings)
                .FirstOrDefaultAsync(j => j.JobId == jobId);
        }

        public async Task<IEnumerable<Job>> GetActiveJobsAsync()
        {
            return await _context.Jobs
                .Include(j => j.Employer)
                    .ThenInclude(e => e.User)
                .Include(j => j.Category)
                .Where(j => j.Status == JobStatus.Active && j.Deadline > DateTime.UtcNow)
                .OrderByDescending(j => j.PostedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByEmployerAsync(string employerId)
        {
            return await _context.Jobs
                .Include(j => j.Category)
                .Include(j => j.Applications)
                .Where(j => j.EmployerId == employerId)
                .OrderByDescending(j => j.PostedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetJobsByCategoryAsync(string categoryId)
        {
            return await _context.Jobs
                .Include(j => j.Employer)
                    .ThenInclude(e => e.User)
                .Include(j => j.Category)
                .Where(j => j.CategoryId == categoryId && j.Status == JobStatus.Active)
                .OrderByDescending(j => j.PostedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>> SearchJobsByLocationAsync(string location)
        {
            return await _context.Jobs
                .Include(j => j.Employer)
                    .ThenInclude(e => e.User)
                .Include(j => j.Category)
                .Where(j => j.Status == JobStatus.Active && 
                           j.Deadline > DateTime.UtcNow &&
                           EF.Functions.Like(j.Location, $"%{location}%"))
                .OrderByDescending(j => j.PostedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>> SearchJobsAsync(
            string searchTerm, 
            string location, 
            string categoryId, 
            decimal? minPay, 
            decimal? maxPay)
        {
            var query = _context.Jobs
                .Include(j => j.Employer)
                    .ThenInclude(e => e.User)
                .Include(j => j.Category)
                .Where(j => j.Status == JobStatus.Active && j.Deadline > DateTime.UtcNow);

            // Search term filter (title or description)
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(j => 
                    EF.Functions.Like(j.Title, $"%{searchTerm}%") ||
                    EF.Functions.Like(j.Description, $"%{searchTerm}%"));
            }

            // Location filter
            if (!string.IsNullOrWhiteSpace(location))
            {
                query = query.Where(j => EF.Functions.Like(j.Location, $"%{location}%"));
            }

            // Category filter
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                query = query.Where(j => j.CategoryId == categoryId);
            }

            // Pay range filter
            if (minPay.HasValue)
            {
                query = query.Where(j => j.PayAmount >= minPay.Value);
            }

            if (maxPay.HasValue)
            {
                query = query.Where(j => j.PayAmount <= maxPay.Value);
            }

            return await query
                .OrderByDescending(j => j.PostedDate)
                .ToListAsync();
        }

        public async Task<Job> AddAsync(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<Job> UpdateAsync(Job job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<bool> DeleteAsync(string jobId)
        {
            var job = await _context.Jobs.FindAsync(jobId);
            if (job == null)
            {
                return false;
            }

            // Soft delete by updating status
            job.Status = JobStatus.Cancelled;
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetActiveJobCountByEmployerAsync(string employerId)
        {
            return await _context.Jobs
                .CountAsync(j => j.EmployerId == employerId && j.Status == JobStatus.Active);
        }
    }
}
