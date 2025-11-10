using Application.Interfaces;
using Domain.Classes;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly PeerPayDbContext _context;

        public JobApplicationRepository(PeerPayDbContext context)
        {
            _context = context;
        }

        public async Task<JobApplication> CreateApplicationAsync(JobApplication application)
        {
            _context.JobApplications.Add(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<JobApplication?> GetApplicationByIdAsync(string applicationId)
        {
            return await _context.JobApplications
                .Include(a => a.Job)
                .Include(a => a.Student)
                    .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(a => a.ApplicationId == applicationId);
        }

        public async Task<List<JobApplication>> GetApplicationsByStudentIdAsync(string studentId)
        {
            return await _context.JobApplications
                .Include(a => a.Job)
                    .ThenInclude(j => j.Employer)
                        .ThenInclude(e => e.User)
                .Include(a => a.Student)
                .Where(a => a.StudentId == studentId)
                .OrderByDescending(a => a.AppliedDate)
                .ToListAsync();
        }

        public async Task<List<JobApplication>> GetApplicationsByJobIdAsync(string jobId)
        {
            return await _context.JobApplications
                .Include(a => a.Student)
                    .ThenInclude(s => s.User)
                .Include(a => a.Job)
                .Where(a => a.JobId == jobId)
                .OrderByDescending(a => a.AppliedDate)
                .ToListAsync();
        }

        public async Task<JobApplication?> GetApplicationByJobAndStudentAsync(string jobId, string studentId)
        {
            return await _context.JobApplications
                .FirstOrDefaultAsync(a => a.JobId == jobId && a.StudentId == studentId);
        }

        public async Task<bool> UpdateApplicationStatusAsync(string applicationId, ApplicationStatus status, string updatedBy, string? employerNotes = null)
        {
            var application = await _context.JobApplications.FindAsync(applicationId);
            if (application == null) return false;

            application.Status = status;
            application.StatusUpdatedAt = DateTime.UtcNow;
            application.UpdatedBy = updatedBy;
            
            if (!string.IsNullOrEmpty(employerNotes))
            {
                application.EmployerNotes = employerNotes;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteApplicationAsync(string applicationId)
        {
            var application = await _context.JobApplications.FindAsync(applicationId);
            if (application == null) return false;

            _context.JobApplications.Remove(application);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetApplicationCountByJobIdAsync(string jobId)
        {
            return await _context.JobApplications
                .CountAsync(a => a.JobId == jobId);
        }

        public async Task<bool> HasStudentAppliedToJobAsync(string studentId, string jobId)
        {
            return await _context.JobApplications
                .AnyAsync(a => a.StudentId == studentId && a.JobId == jobId);
        }
    }
}
