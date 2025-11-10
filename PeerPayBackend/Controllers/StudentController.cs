using Application.Commands.StudentCommand;
using Application.Queries.StudentQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PeerPayBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IMediator mediator, ILogger<StudentController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Register a new student
        /// POST /api/student/register
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterStudentCommand command)
        {
            try
            {
                _logger.LogInformation("Student registration attempt for email: {Email}", command.Email);
                var result = await _mediator.Send(command);
                _logger.LogInformation("Student registered successfully: {UserId}", result.UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering student with email: {Email}", command.Email);
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get student profile by ID
        /// GET /api/student/{id}
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(string id)
        {
            try
            {
                var query = new GetStudentByIdQuery { StudentId = id };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update student profile
        /// PUT /api/student/{id}
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudentProfile(string id, [FromBody] UpdateStudentProfileCommand command)
        {
            try
            {
                if (id != command.StudentId)
                {
                    command.StudentId = id;
                }

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
