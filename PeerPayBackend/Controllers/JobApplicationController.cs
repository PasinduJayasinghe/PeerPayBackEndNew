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
        public async Task<ActionResult<JobApplicationDto>> ApplyToJob([FromBody] ApplyToJobCommand command)
        {
            try
            {
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
        /// GET /api/jobapplication/student/{studentId}
        /// </summary>
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<List<JobApplicationDto>>> GetStudentApplications(string studentId)
        {
            try
            {
                var query = new GetStudentApplicationsQuery { StudentId = studentId };
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
