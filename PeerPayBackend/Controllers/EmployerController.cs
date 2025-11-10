using Application.Commands.EmployerCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PeerPayBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmployerController> _logger;

        public EmployerController(IMediator mediator, ILogger<EmployerController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Register a new employer
        /// POST /api/employer/register
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterEmployerCommand command)
        {
            try
            {
                _logger.LogInformation("Received employer registration request for email: {Email}", command.Email);
                var result = await _mediator.Send(command);
                _logger.LogInformation("Employer registered successfully: {UserId}", result.UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering employer with email: {Email}", command.Email);
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
