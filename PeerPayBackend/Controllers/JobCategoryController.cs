using Application.Dtos;
using Application.Queries.JobCategoryQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PeerPayBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<JobCategoryController> _logger;

        public JobCategoryController(IMediator mediator, ILogger<JobCategoryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get all job categories
        /// GET /api/jobcategory
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobCategoryDto>>> GetAllCategories()
        {
            try
            {
                _logger.LogInformation("Fetching all job categories");
                var query = new GetAllJobCategoriesQuery();
                var result = await _mediator.Send(query);
                _logger.LogInformation("Retrieved {Count} job categories", result.Count());
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching job categories");
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
