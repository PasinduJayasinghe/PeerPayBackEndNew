using Application.Interfaces;
using Domain.Classes;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly PeerPayDbContext _context;

        public RatingRepository(PeerPayDbContext context)
        {
            _context = context;
        }

        public async Task<Rating> GetByIdAsync(string ratingId)
        {
            return await _context.Ratings
                .Include(r => r.Rater)
                .Include(r => r.RatedUser)
                .Include(r => r.Job)
                .FirstOrDefaultAsync(r => r.RatingId == ratingId);
        }

        public async Task<IEnumerable<Rating>> GetByJobIdAsync(string jobId)
        {
            return await _context.Ratings
                .Where(r => r.JobId == jobId)
                .Include(r => r.Rater)
                .Include(r => r.RatedUser)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Rating>> GetByRatedUserIdAsync(string userId, int pageNumber = 1, int pageSize = 20)
        {
            return await _context.Ratings
                .Where(r => r.RatedUserId == userId && r.IsPublic)
                .Include(r => r.Rater)
                .Include(r => r.Job)
                .OrderByDescending(r => r.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Rating>> GetByRaterIdAsync(string raterId)
        {
            return await _context.Ratings
                .Where(r => r.RaterId == raterId)
                .Include(r => r.RatedUser)
                .Include(r => r.Job)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<Rating> GetByJobAndRaterAsync(string jobId, string raterId)
        {
            return await _context.Ratings
                .FirstOrDefaultAsync(r => r.JobId == jobId && r.RaterId == raterId);
        }

        public async Task<double> GetAverageRatingAsync(string userId)
        {
            var ratings = await _context.Ratings
                .Where(r => r.RatedUserId == userId)
                .ToListAsync();

            if (!ratings.Any())
                return 0;

            return ratings.Average(r => r.RatingValue);
        }

        public async Task<Dictionary<int, int>> GetRatingDistributionAsync(string userId)
        {
            var ratings = await _context.Ratings
                .Where(r => r.RatedUserId == userId)
                .GroupBy(r => r.RatingValue)
                .Select(g => new { RatingValue = g.Key, Count = g.Count() })
                .ToListAsync();

            var distribution = new Dictionary<int, int>
            {
                { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }
            };

            foreach (var rating in ratings)
            {
                distribution[rating.RatingValue] = rating.Count;
            }

            return distribution;
        }

        public async Task<int> GetTotalRatingsCountAsync(string userId)
        {
            return await _context.Ratings
                .CountAsync(r => r.RatedUserId == userId);
        }

        public async Task<Rating> AddAsync(Rating rating)
        {
            rating.CreatedAt = DateTime.UtcNow;
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task UpdateAsync(Rating rating)
        {
            rating.UpdatedAt = DateTime.UtcNow;
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string ratingId)
        {
            var rating = await _context.Ratings.FindAsync(ratingId);
            if (rating != null)
            {
                _context.Ratings.Remove(rating);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> HasUserRatedJobAsync(string jobId, string raterId)
        {
            return await _context.Ratings
                .AnyAsync(r => r.JobId == jobId && r.RaterId == raterId);
        }
    }
}
