using Application.Commands.RatingCommand;
using Application.Dtos;
using Application.Queries.RatingQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerPayBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RatingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/rating
        [HttpPost]
        public async Task<IActionResult> CreateRating([FromBody] CreateRatingDto dto)
        {
            var command = new CreateRatingCommand
            {
                JobId = dto.JobId,
                RaterId = dto.RaterId,
                RatedUserId = dto.RatedUserId,
                RatingValue = dto.RatingValue,
                Review = dto.Review,
                RatingType = dto.RatingType,
                IsPublic = dto.IsPublic
            };

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetRatingById), new { id = result.RatingId }, result);
        }

        // GET: api/rating/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRatingById(string id)
        {
            var query = new GetRatingByIdQuery { RatingId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound(new { message = "Rating not found" });
            }

            return Ok(result);
        }

        // GET: api/rating/job/{jobId}
        [HttpGet("job/{jobId}")]
        public async Task<IActionResult> GetJobRatings(string jobId)
        {
            var query = new GetJobRatingsQuery { JobId = jobId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/rating/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserRatings(
            string userId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var query = new GetUserRatingsQuery
            {
                UserId = userId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/rating/user/{userId}/stats
        [HttpGet("user/{userId}/stats")]
        public async Task<IActionResult> GetUserRatingStats(string userId)
        {
            var query = new GetUserRatingStatsQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/rating/rater/{raterId}
        [HttpGet("rater/{raterId}")]
        public async Task<IActionResult> GetRatingsByRater(string raterId)
        {
            var query = new GetRatingsByRaterQuery { RaterId = raterId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/rating/check
        [HttpGet("check")]
        public async Task<IActionResult> CheckUserRatedJob(
            [FromQuery] string jobId,
            [FromQuery] string raterId)
        {
            var query = new CheckUserRatedJobQuery
            {
                JobId = jobId,
                RaterId = raterId
            };

            var result = await _mediator.Send(query);
            return Ok(new { hasRated = result });
        }

        // PUT: api/rating/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRating(string id, [FromBody] UpdateRatingDto dto)
        {
            var command = new UpdateRatingCommand
            {
                RatingId = id,
                RatingValue = dto.RatingValue,
                Review = dto.Review,
                IsPublic = dto.IsPublic
            };

            var result = await _mediator.Send(command);
            return Ok(new { success = result, message = "Rating updated successfully" });
        }

        // DELETE: api/rating/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(string id)
        {
            var command = new DeleteRatingCommand { RatingId = id };
            var result = await _mediator.Send(command);
            return Ok(new { success = result, message = "Rating deleted successfully" });
        }
    }
}
