using Application.Interfaces;
using Domain.Classes;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmployerRepository : IEmployerRepository
    {
        private readonly PeerPayDbContext _context;

        public EmployerRepository(PeerPayDbContext context)
        {
            _context = context;
        }

        public async Task<Employer> GetByIdAsync(string employerId)
        {
            return await _context.Employers
                .Include(e => e.User)
                .ThenInclude(u => u.Profile)
                .Include(e => e.Jobs)
                .FirstOrDefaultAsync(e => e.EmployerId == employerId);
        }

        public async Task<Employer> GetByUserIdAsync(string userId)
        {
            return await _context.Employers
                .Include(e => e.User)
                .ThenInclude(u => u.Profile)
                .Include(e => e.Jobs)
                .Include(e => e.Payments)
                .FirstOrDefaultAsync(e => e.UserId == userId);
        }

        public async Task<IEnumerable<Employer>> GetVerifiedEmployersAsync()
        {
            return await _context.Employers
                .Include(e => e.User)
                .Where(e => e.VerificationStatus == "Verified")
                .ToListAsync();
        }

        public async Task<IEnumerable<Employer>> GetAllAsync()
        {
            return await _context.Employers
                .Include(e => e.User)
                .ToListAsync();
        }

        public async Task<Employer> AddAsync(Employer employer)
        {
            employer.EmployerId = Guid.NewGuid().ToString();
            employer.Rating = 0;
            employer.JobsPosted = 0;
            employer.VerificationStatus = "Pending";

            await _context.Employers.AddAsync(employer);
            await _context.SaveChangesAsync();

            return employer;
        }

        public async Task UpdateAsync(Employer employer)
        {
            _context.Employers.Update(employer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string employerId)
        {
            var employer = await _context.Employers.FindAsync(employerId);
            if (employer != null)
            {
                _context.Employers.Remove(employer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string employerId)
        {
            return await _context.Employers.AnyAsync(e => e.EmployerId == employerId);
        }

        public async Task<IEnumerable<Employer>> GetByCompanyTypeAsync(string companyType)
        {
            return await _context.Employers
                .Include(e => e.User)
                .Where(e => e.CompanyType.ToLower() == companyType.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<Employer>> GetTopRatedEmployersAsync(int count)
        {
            return await _context.Employers
                .Include(e => e.User)
                .OrderByDescending(e => e.Rating)
                .Take(count)
                .ToListAsync();
        }

        public async Task UpdateRatingAsync(string employerId, decimal newRating)
        {
            var employer = await _context.Employers.FindAsync(employerId);
            if (employer != null)
            {
                employer.Rating = newRating;
                await _context.SaveChangesAsync();
            }
        }

        public async Task IncrementJobsPostedAsync(string employerId)
        {
            var employer = await _context.Employers.FindAsync(employerId);
            if (employer != null)
            {
                employer.JobsPosted++;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateVerificationStatusAsync(string employerId, string status)
        {
            var employer = await _context.Employers.FindAsync(employerId);
            if (employer != null)
            {
                employer.VerificationStatus = status;
                await _context.SaveChangesAsync();
            }
        }
    }
}
