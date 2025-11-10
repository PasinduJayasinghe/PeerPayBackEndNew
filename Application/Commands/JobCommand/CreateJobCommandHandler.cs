using Application.Commands.JobCommand;
using Application.Dtos;
using Application.Interfaces;
using Domain.Classes;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.JobCommand
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, JobDto>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IEmployerRepository _employerRepository;
        private readonly ILogger<CreateJobCommandHandler> _logger;

        public CreateJobCommandHandler(
            IJobRepository jobRepository,
            IEmployerRepository employerRepository,
            ILogger<CreateJobCommandHandler> logger)
        {
            _jobRepository = jobRepository;
            _employerRepository = employerRepository;
            _logger = logger;
        }

        public async Task<JobDto> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating job: {Title} for employer: {EmployerId}", request.Title, request.EmployerId);
            
            // Verify employer exists
            var employer = await _employerRepository.GetByUserIdAsync(request.EmployerId);
            if (employer == null)
            {
                _logger.LogWarning("Job creation failed: Employer {EmployerId} not found", request.EmployerId);
                throw new Exception("Employer not found");
            }

            // Create Job entity
            var job = new Job
            {
                JobId = Guid.NewGuid().ToString(),
                EmployerId = employer.EmployerId, // Use the actual EmployerId from the employer entity
                CategoryId = request.CategoryId,
                Title = request.Title,
                Description = request.Description,
                PayAmount = request.PayAmount,
                PayType = request.PayType,
                DurationDays = request.DurationDays,
                RequiredSkills = request.RequiredSkills,
                Attachments = new string[] { },
                PostedDate = DateTime.UtcNow,
                Deadline = request.Deadline,
                Status = JobStatus.Active,
                Location = request.Location,
                JobType = request.JobType,
                MaxApplicants = request.MaxApplicants
            };

            // Save to database
            var createdJob = await _jobRepository.AddAsync(job);

            // Update employer's job count
            employer.JobsPosted++;
            // Note: You might want to add an UpdateAsync method to EmployerRepository

            // Map to DTO
            return new JobDto
            {
                JobId = createdJob.JobId,
                EmployerId = createdJob.EmployerId,
                CategoryId = createdJob.CategoryId,
                Title = createdJob.Title,
                Description = createdJob.Description,
                PayAmount = createdJob.PayAmount,
                PayType = createdJob.PayType,
                DurationDays = createdJob.DurationDays,
                RequiredSkills = createdJob.RequiredSkills,
                PostedDate = createdJob.PostedDate,
                Deadline = createdJob.Deadline,
                Status = createdJob.Status,
                Location = createdJob.Location,
                JobType = createdJob.JobType,
                MaxApplicants = createdJob.MaxApplicants,
                ApplicationCount = 0
            };
        }
    }
}
