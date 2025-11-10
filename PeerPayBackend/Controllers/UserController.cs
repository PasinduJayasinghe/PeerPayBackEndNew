using Application.Commands.LoginCommand;
using Application.Commands.UserCommand;
using Application.Dtos;
using Application.Queries.UserQuery;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PeerPayBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Login user with email/phone and password
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginUserCommand command)
        {
            try
            {
                _logger.LogInformation("Login attempt for user: {EmailOrPhone}", command.EmailOrPhone);
                var result = await _mediator.Send(command);
                _logger.LogInformation("User logged in successfully: {Email}", result.User.Email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Login failed for user: {EmailOrPhone}", command.EmailOrPhone);
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(string id)
        {
            try
            {
                var query = new GetUserByIdQuery { UserId = id };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            try
            {
                var query = new GetUserByEmailQuery { Email = email };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get all users
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            try
            {
                var query = new GetAllUsersQuery();
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get users by status
        /// </summary>
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersByStatus(UserStatus status)
        {
            try
            {
                var query = new GetUsersByStatusQuery { Status = status };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Get users by type
        /// </summary>
        [HttpGet("type/{userType}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersByType(UserType userType)
        {
            try
            {
                var query = new GetUsersByTypeQuery { UserType = userType };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update user profile
        /// </summary>
        [HttpPut("{id}/profile")]
        public async Task<ActionResult<UserDto>> UpdateProfile(string id, [FromBody] UpdateUserProfileCommand command)
        {
            try
            {
                if (id != command.UserId)
                {
                    return BadRequest(new { error = "User ID mismatch" });
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
        /// Change user password
        /// </summary>
        [HttpPost("{id}/change-password")]
        public async Task<ActionResult<bool>> ChangePassword(string id, [FromBody] ChangePasswordCommand command)
        {
            try
            {
                if (id != command.UserId)
                {
                    return BadRequest(new { error = "User ID mismatch" });
                }

                var result = await _mediator.Send(command);
                return Ok(new { success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Update user status (Admin only)
        /// </summary>
        [HttpPut("{id}/status")]
        public async Task<ActionResult<bool>> UpdateStatus(string id, [FromBody] UpdateUserStatusCommand command)
        {
            try
            {
                if (id != command.UserId)
                {
                    return BadRequest(new { error = "User ID mismatch" });
                }

                var result = await _mediator.Send(command);
                return Ok(new { success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Verify user (Admin only)
        /// </summary>
        [HttpPost("{id}/verify")]
        public async Task<ActionResult<bool>> VerifyUser(string id, [FromBody] VerifyUserCommand command)
        {
            try
            {
                if (id != command.UserId)
                {
                    return BadRequest(new { error = "User ID mismatch" });
                }

                var result = await _mediator.Send(command);
                return Ok(new { success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Delete user (Admin only)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUser(string id)
        {
            try
            {
                var command = new DeleteUserCommand { UserId = id };
                var result = await _mediator.Send(command);
                return Ok(new { success = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
