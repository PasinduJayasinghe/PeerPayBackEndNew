using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJobRepository
    {
        Task<Job> GetByIdAsync(string jobId);
        Task<IEnumerable<Job>> GetAllAsync();
        Task<IEnumerable<Job>> GetActiveJobsAsync();
        Task<IEnumerable<Job>> GetJobsByEmployerAsync(string employerId);
        Task<IEnumerable<Job>> GetJobsByCategoryAsync(string categoryId);
        Task<IEnumerable<Job>> SearchJobsByLocationAsync(string location);
        Task<IEnumerable<Job>> SearchJobsAsync(string searchTerm, string location, string categoryId, decimal? minPay, decimal? maxPay);
        Task<Job> AddAsync(Job job);
        Task<Job> UpdateAsync(Job job);
        Task<bool> DeleteAsync(string jobId);
        Task<int> GetActiveJobCountByEmployerAsync(string employerId);
    }
}
