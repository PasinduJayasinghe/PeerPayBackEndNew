using Domain.Classes;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRatingRepository
    {
        Task<Rating> GetByIdAsync(string ratingId);
        Task<IEnumerable<Rating>> GetByJobIdAsync(string jobId);
        Task<IEnumerable<Rating>> GetByRatedUserIdAsync(string userId, int pageNumber = 1, int pageSize = 20);
        Task<IEnumerable<Rating>> GetByRaterIdAsync(string raterId);
        Task<Rating> GetByJobAndRaterAsync(string jobId, string raterId);
        Task<double> GetAverageRatingAsync(string userId);
        Task<Dictionary<int, int>> GetRatingDistributionAsync(string userId);
        Task<int> GetTotalRatingsCountAsync(string userId);
        Task<Rating> AddAsync(Rating rating);
        Task UpdateAsync(Rating rating);
        Task DeleteAsync(string ratingId);
        Task<bool> HasUserRatedJobAsync(string jobId, string raterId);
    }
}
