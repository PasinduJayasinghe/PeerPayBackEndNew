using Application.Commands.JobCommand;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.JobCommand
{
    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, bool>
    {
        private readonly IJobRepository _jobRepository;

        public DeleteJobCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<bool> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            // Get the job to verify ownership
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job == null)
            {
                throw new Exception("Job not found");
            }

            // Verify that the employer owns this job
            if (job.EmployerId != request.EmployerId)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this job");
            }

            // Delete (soft delete by updating status to Cancelled)
            var result = await _jobRepository.DeleteAsync(request.JobId);
            return result;
        }
    }
}
