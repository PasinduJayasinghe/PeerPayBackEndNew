using Application.Commands.OTPCommand;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PeerPayBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OTPController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OTPController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Send OTP to email for verification
        /// </summary>
        [HttpPost("send-otp")]
        public async Task<ActionResult<OtpResponseDto>> SendOtp([FromBody] SendOtpCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Verify OTP code
        /// </summary>
        [HttpPost("verify-otp")]
        public async Task<ActionResult<OtpResponseDto>> VerifyOtp([FromBody] VerifyOtpCommand command)
        {
            try
            {
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
