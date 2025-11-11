using Application.Commands.JobApplicationCommand;
using Application.Dtos;
using Application.Queries.JobApplicationQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PeerPayBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobApplicationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Apply to a job
        /// POST /api/jobapplication/apply
        /// </summary>
        [HttpPost("apply")]
        public async Task<ActionResult<JobApplicationDto>> ApplyToJob([FromBody] ApplyToJobDto dto)
        {
            try
            {
                // Map DTO to Command (studentId from frontend -> UserId in backend)
                var command = new ApplyToJobCommand
                {
                    JobId = dto.JobId,
                    UserId = dto.StudentId, // Frontend sends studentId but value is actually UserId
                    CoverLetter = dto.CoverLetter
                };

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException != null 
                    ? $"{ex.Message} Inner: {ex.InnerException.Message}" 
                    : ex.Message;
                return BadRequest(new { error = errorMessage });
            }
        }

        /// <summary>
        /// Get all applications for a student
        /// GET /api/jobapplication/student/{userId}
        /// </summary>
        [HttpGet("student/{userId}")]
        public async Task<ActionResult<List<JobApplicationDto>>> GetStudentApplications(string userId)
        {
            try
            {
                var query = new GetStudentApplicationsQuery { UserId = userId };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get all applications for a job
        /// GET /api/jobapplication/job/{jobId}
        /// </summary>
        [HttpGet("job/{jobId}")]
        public async Task<ActionResult<List<JobApplicationDto>>> GetJobApplications(string jobId)
        {
            try
            {
                var query = new GetJobApplicationsQuery { JobId = jobId };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update application status (Accept/Reject)
        /// PUT /api/jobapplication/{id}/status
        /// </summary>
        [HttpPut("{id}/status")]
        public async Task<ActionResult<bool>> UpdateApplicationStatus(string id, [FromBody] UpdateApplicationStatusCommand command)
        {
            try
            {
                if (id != command.ApplicationId)
                {
                    return BadRequest(new { error = "Application ID mismatch" });
                }

                var result = await _mediator.Send(command);
                return Ok(new { success = result });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
