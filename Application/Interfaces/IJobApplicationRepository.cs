using Domain.Classes;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface IJobApplicationRepository
    {
        Task<JobApplication> CreateApplicationAsync(JobApplication application);
        Task<JobApplication?> GetApplicationByIdAsync(string applicationId);
        Task<List<JobApplication>> GetApplicationsByUserIdAsync(string userId);
        Task<List<JobApplication>> GetApplicationsByJobIdAsync(string jobId);
        Task<JobApplication?> GetApplicationByJobAndUserAsync(string jobId, string userId);
        Task<bool> UpdateApplicationStatusAsync(string applicationId, ApplicationStatus status, string updatedBy, string? employerNotes = null);
        Task<bool> DeleteApplicationAsync(string applicationId);
        Task<int> GetApplicationCountByJobIdAsync(string jobId);
        Task<bool> HasUserAppliedToJobAsync(string userId, string jobId);
    }
}
