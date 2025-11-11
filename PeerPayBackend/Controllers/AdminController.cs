using Application.Dtos;
using Application.Queries.AdminQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PeerPayBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get admin dashboard data with statistics and recent items
        /// GET /api/admin/dashboard
        /// </summary>
        [HttpGet("dashboard")]
        public async Task<ActionResult<AdminDashboardDto>> GetDashboard([FromQuery] int recentItemsCount = 10)
        {
            try
            {
                var query = new GetAdminDashboardQuery { RecentItemsCount = recentItemsCount };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get all employer accounts
        /// GET /api/admin/employers
        /// </summary>
        [HttpGet("employers")]
        public async Task<ActionResult<List<EmployerAccountDto>>> GetAllEmployers()
        {
            try
            {
                var query = new GetAllEmployersQuery();
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get all student accounts
        /// GET /api/admin/students
        /// </summary>
        [HttpGet("students")]
        public async Task<ActionResult<List<StudentAccountDto>>> GetAllStudents()
        {
            try
            {
                var query = new GetAllStudentsQuery();
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get all jobs
        /// GET /api/admin/jobs
        /// </summary>
        [HttpGet("jobs")]
        public async Task<ActionResult<List<JobDto>>> GetAllJobs()
        {
            try
            {
                var query = new GetAllJobsQuery();
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get all job applications
        /// GET /api/admin/applications
        /// </summary>
        [HttpGet("applications")]
        public async Task<ActionResult<List<JobApplicationDto>>> GetAllApplications()
        {
            try
            {
                var query = new GetAllApplicationsQuery();
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
