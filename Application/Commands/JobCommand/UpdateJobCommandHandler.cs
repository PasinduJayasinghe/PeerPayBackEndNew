using Application.Commands.JobCommand;
using Application.Dtos;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.JobCommand
{
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, JobDto>
    {
        private readonly IJobRepository _jobRepository;

        public UpdateJobCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<JobDto> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            // Get existing job
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job == null)
            {
                throw new Exception("Job not found");
            }

            // Update job properties
            job.CategoryId = request.CategoryId;
            job.Title = request.Title;
            job.Description = request.Description;
            job.PayAmount = request.PayAmount;
            job.PayType = request.PayType;
            job.DurationDays = request.DurationDays;
            job.RequiredSkills = request.RequiredSkills;
            job.Deadline = request.Deadline;
            job.Location = request.Location;
            job.JobType = request.JobType;
            job.MaxApplicants = request.MaxApplicants;
            job.Status = request.Status;

            // Save changes
            var updatedJob = await _jobRepository.UpdateAsync(job);

            // Map to DTO
            return new JobDto
            {
                JobId = updatedJob.JobId,
                EmployerId = updatedJob.EmployerId,
                EmployerName = updatedJob.Employer?.User?.Name,
                CompanyName = updatedJob.Employer?.CompanyName,
                CategoryId = updatedJob.CategoryId,
                CategoryName = updatedJob.Category?.Name,
                Title = updatedJob.Title,
                Description = updatedJob.Description,
                PayAmount = updatedJob.PayAmount,
                PayType = updatedJob.PayType,
                DurationDays = updatedJob.DurationDays,
                RequiredSkills = updatedJob.RequiredSkills,
                PostedDate = updatedJob.PostedDate,
                Deadline = updatedJob.Deadline,
                Status = updatedJob.Status,
                Location = updatedJob.Location,
                JobType = updatedJob.JobType,
                MaxApplicants = updatedJob.MaxApplicants,
                ApplicationCount = updatedJob.Applications?.Count ?? 0
            };
        }
    }
}
