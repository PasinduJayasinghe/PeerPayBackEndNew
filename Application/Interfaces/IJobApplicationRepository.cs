using Domain.Classes;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface IJobApplicationRepository
    {
        Task<JobApplication> CreateApplicationAsync(JobApplication application);
        Task<JobApplication?> GetApplicationByIdAsync(string applicationId);
        Task<List<JobApplication>> GetApplicationsByStudentIdAsync(string studentId);
        Task<List<JobApplication>> GetApplicationsByJobIdAsync(string jobId);
        Task<JobApplication?> GetApplicationByJobAndStudentAsync(string jobId, string studentId);
        Task<bool> UpdateApplicationStatusAsync(string applicationId, ApplicationStatus status, string updatedBy, string? employerNotes = null);
        Task<bool> DeleteApplicationAsync(string applicationId);
        Task<int> GetApplicationCountByJobIdAsync(string jobId);
        Task<bool> HasStudentAppliedToJobAsync(string studentId, string jobId);
    }
}
