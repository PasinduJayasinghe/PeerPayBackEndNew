using Application.Commands.JobCommand;
using Application.Dtos;
using Application.Queries.JobQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PeerPayBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<JobController> _logger;

        public JobController(IMediator mediator, ILogger<JobController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Create a new job posting
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<JobDto>> CreateJob([FromBody] CreateJobCommand command)
        {
            try
            {
                _logger.LogInformation("Creating job: {Title} for employer: {EmployerId}", command.Title, command.EmployerId);
                var result = await _mediator.Send(command);
                _logger.LogInformation("Job created successfully: {JobId}", result.JobId);
                return CreatedAtAction(nameof(GetJobById), new { id = result.JobId }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating job: {Title}", command.Title);
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing job
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<JobDto>> UpdateJob(string id, [FromBody] UpdateJobCommand command)
        {
            try
            {
                if (id != command.JobId)
                {
                    return BadRequest(new { error = "Job ID mismatch" });
                }

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Delete a job (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteJob(string id, [FromQuery] string employerId)
        {
            try
            {
                var command = new DeleteJobCommand 
                { 
                    JobId = id, 
                    EmployerId = employerId 
                };
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

        /// <summary>
        /// Get a specific job by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<JobDto>> GetJobById(string id)
        {
            try
            {
                var query = new GetJobByIdQuery { JobId = id };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get all active jobs
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDto>>> GetActiveJobs()
        {
            try
            {
                var query = new GetActiveJobsQuery();
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get jobs by employer ID
        /// </summary>
        [HttpGet("employer/{employerId}")]
        public async Task<ActionResult<IEnumerable<JobDto>>> GetJobsByEmployer(string employerId)
        {
            try
            {
                var query = new GetJobsByEmployerQuery { EmployerId = employerId };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get jobs by category ID
        /// </summary>
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<JobDto>>> GetJobsByCategory(string categoryId)
        {
            try
            {
                var query = new GetJobsByCategoryQuery { CategoryId = categoryId };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Search jobs by location
        /// </summary>
        [HttpGet("location/{location}")]
        public async Task<ActionResult<IEnumerable<JobDto>>> SearchJobsByLocation(string location)
        {
            try
            {
                var query = new SearchJobsByLocationQuery { Location = location };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Advanced job search with multiple filters
        /// </summary>
        [HttpPost("search")]
        public async Task<ActionResult<IEnumerable<JobDto>>> SearchJobs([FromBody] SearchJobsQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
