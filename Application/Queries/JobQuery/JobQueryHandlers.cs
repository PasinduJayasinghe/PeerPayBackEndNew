using Application.Dtos;
using Application.Interfaces;
using Application.Queries.JobQuery;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.JobQuery
{
    public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, JobDto>
    {
        private readonly IJobRepository _jobRepository;

        public GetJobByIdQueryHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<JobDto> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job == null)
            {
                throw new Exception("Job not found");
            }

            return new JobDto
            {
                JobId = job.JobId,
                EmployerId = job.EmployerId,
                EmployerName = job.Employer?.User?.Name,
                CompanyName = job.Employer?.CompanyName,
                CategoryId = job.CategoryId,
                CategoryName = job.Category?.Name,
                Title = job.Title,
                Description = job.Description,
                PayAmount = job.PayAmount,
                PayType = job.PayType,
                DurationDays = job.DurationDays,
                RequiredSkills = job.RequiredSkills,
                PostedDate = job.PostedDate,
                Deadline = job.Deadline,
                Status = job.Status,
                Location = job.Location,
                JobType = job.JobType,
                MaxApplicants = job.MaxApplicants,
                ApplicationCount = job.Applications?.Count ?? 0
            };
        }
    }

    public class GetActiveJobsQueryHandler : IRequestHandler<GetActiveJobsQuery, IEnumerable<JobDto>>
    {
        private readonly IJobRepository _jobRepository;

        public GetActiveJobsQueryHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<IEnumerable<JobDto>> Handle(GetActiveJobsQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetActiveJobsAsync();
            
            return jobs.Select(job => new JobDto
            {
                JobId = job.JobId,
                EmployerId = job.EmployerId,
                EmployerName = job.Employer?.User?.Name,
                CompanyName = job.Employer?.CompanyName,
                CategoryId = job.CategoryId,
                CategoryName = job.Category?.Name,
                Title = job.Title,
                Description = job.Description,
                PayAmount = job.PayAmount,
                PayType = job.PayType,
                DurationDays = job.DurationDays,
                RequiredSkills = job.RequiredSkills,
                PostedDate = job.PostedDate,
                Deadline = job.Deadline,
                Status = job.Status,
                Location = job.Location,
                JobType = job.JobType,
                MaxApplicants = job.MaxApplicants,
                ApplicationCount = job.Applications?.Count ?? 0
            }).ToList();
        }
    }

    public class GetJobsByEmployerQueryHandler : IRequestHandler<GetJobsByEmployerQuery, IEnumerable<JobDto>>
    {
        private readonly IJobRepository _jobRepository;

        public GetJobsByEmployerQueryHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<IEnumerable<JobDto>> Handle(GetJobsByEmployerQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetJobsByEmployerAsync(request.EmployerId);
            
            return jobs.Select(job => new JobDto
            {
                JobId = job.JobId,
                EmployerId = job.EmployerId,
                CategoryId = job.CategoryId,
                CategoryName = job.Category?.Name,
                Title = job.Title,
                Description = job.Description,
                PayAmount = job.PayAmount,
                PayType = job.PayType,
                DurationDays = job.DurationDays,
                RequiredSkills = job.RequiredSkills,
                PostedDate = job.PostedDate,
                Deadline = job.Deadline,
                Status = job.Status,
                Location = job.Location,
                JobType = job.JobType,
                MaxApplicants = job.MaxApplicants,
                ApplicationCount = job.Applications?.Count ?? 0
            }).ToList();
        }
    }

    public class GetJobsByCategoryQueryHandler : IRequestHandler<GetJobsByCategoryQuery, IEnumerable<JobDto>>
    {
        private readonly IJobRepository _jobRepository;

        public GetJobsByCategoryQueryHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<IEnumerable<JobDto>> Handle(GetJobsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetJobsByCategoryAsync(request.CategoryId);
            
            return jobs.Select(job => new JobDto
            {
                JobId = job.JobId,
                EmployerId = job.EmployerId,
                EmployerName = job.Employer?.User?.Name,
                CompanyName = job.Employer?.CompanyName,
                CategoryId = job.CategoryId,
                CategoryName = job.Category?.Name,
                Title = job.Title,
                Description = job.Description,
                PayAmount = job.PayAmount,
                PayType = job.PayType,
                DurationDays = job.DurationDays,
                RequiredSkills = job.RequiredSkills,
                PostedDate = job.PostedDate,
                Deadline = job.Deadline,
                Status = job.Status,
                Location = job.Location,
                JobType = job.JobType,
                MaxApplicants = job.MaxApplicants,
                ApplicationCount = job.Applications?.Count ?? 0
            }).ToList();
        }
    }

    public class SearchJobsByLocationQueryHandler : IRequestHandler<SearchJobsByLocationQuery, IEnumerable<JobDto>>
    {
        private readonly IJobRepository _jobRepository;

        public SearchJobsByLocationQueryHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<IEnumerable<JobDto>> Handle(SearchJobsByLocationQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.SearchJobsByLocationAsync(request.Location);
            
            return jobs.Select(job => new JobDto
            {
                JobId = job.JobId,
                EmployerId = job.EmployerId,
                EmployerName = job.Employer?.User?.Name,
                CompanyName = job.Employer?.CompanyName,
                CategoryId = job.CategoryId,
                CategoryName = job.Category?.Name,
                Title = job.Title,
                Description = job.Description,
                PayAmount = job.PayAmount,
                PayType = job.PayType,
                DurationDays = job.DurationDays,
                RequiredSkills = job.RequiredSkills,
                PostedDate = job.PostedDate,
                Deadline = job.Deadline,
                Status = job.Status,
                Location = job.Location,
                JobType = job.JobType,
                MaxApplicants = job.MaxApplicants,
                ApplicationCount = job.Applications?.Count ?? 0
            }).ToList();
        }
    }

    public class SearchJobsQueryHandler : IRequestHandler<SearchJobsQuery, IEnumerable<JobDto>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<SearchJobsQueryHandler> _logger;

        public SearchJobsQueryHandler(
            IJobRepository jobRepository,
            ILogger<SearchJobsQueryHandler> logger)
        {
            _jobRepository = jobRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<JobDto>> Handle(SearchJobsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Searching jobs with criteria - SearchTerm: {SearchTerm}, Location: {Location}, CategoryId: {CategoryId}", 
                    request.SearchTerm, request.Location, request.CategoryId);

                // Get all active jobs
                var jobs = await _jobRepository.GetActiveJobsAsync();
                var jobsList = jobs.ToList();

                // Apply keyword search if provided
                if (!string.IsNullOrWhiteSpace(request.SearchTerm))
                {
                    var searchTerm = request.SearchTerm.ToLower();
                    jobsList = jobsList.Where(j =>
                        (j.Title != null && j.Title.ToLower().Contains(searchTerm)) ||
                        (j.Description != null && j.Description.ToLower().Contains(searchTerm)) ||
                        (j.RequiredSkills != null && j.RequiredSkills.Any(skill => skill.ToLower().Contains(searchTerm))) ||
                        (j.Category != null && j.Category.Name.ToLower().Contains(searchTerm))
                    ).ToList();
                }

                // Apply location filter if provided
                if (!string.IsNullOrWhiteSpace(request.Location))
                {
                    jobsList = jobsList.Where(j => 
                        j.Location != null && j.Location.ToLower().Contains(request.Location.ToLower())
                    ).ToList();
                }

                // Apply category filter if provided
                if (!string.IsNullOrWhiteSpace(request.CategoryId))
                {
                    jobsList = jobsList.Where(j => j.CategoryId == request.CategoryId).ToList();
                }

                // Apply pay range filters if provided
                if (request.MinPay.HasValue)
                {
                    jobsList = jobsList.Where(j => j.PayAmount >= request.MinPay.Value).ToList();
                }

                if (request.MaxPay.HasValue)
                {
                    jobsList = jobsList.Where(j => j.PayAmount <= request.MaxPay.Value).ToList();
                }

                _logger.LogInformation("Found {Count} jobs matching criteria", jobsList.Count);

                // Apply pagination
                var paginatedResults = jobsList
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                return paginatedResults.Select(job => new JobDto
                {
                    JobId = job.JobId,
                    EmployerId = job.EmployerId,
                    EmployerName = job.Employer?.User?.Name,
                    CompanyName = job.Employer?.CompanyName,
                    CategoryId = job.CategoryId,
                    CategoryName = job.Category?.Name,
                    Title = job.Title,
                    Description = job.Description,
                    PayAmount = job.PayAmount,
                    PayType = job.PayType,
                    DurationDays = job.DurationDays,
                    RequiredSkills = job.RequiredSkills,
                    PostedDate = job.PostedDate,
                    Deadline = job.Deadline,
                    Status = job.Status,
                    Location = job.Location,
                    JobType = job.JobType,
                    MaxApplicants = job.MaxApplicants,
                    ApplicationCount = job.Applications?.Count ?? 0
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching jobs");
                throw;
            }
        }
    }
}
