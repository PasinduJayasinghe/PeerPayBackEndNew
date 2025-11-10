using Application.Commands.NotificationCommand;
using Application.Dtos;
using Application.Queries.NotificationQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PeerPayBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/notification
        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationDto dto)
        {
            var command = new CreateNotificationCommand
            {
                UserId = dto.UserId,
                Title = dto.Title,
                Content = dto.Content,
                Type = dto.Type,
                ActionUrl = dto.ActionUrl,
                Metadata = dto.Metadata,
                ExpiresAt = dto.ExpiresAt
            };

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetNotificationById), new { id = result.NotificationId }, result);
        }

        // GET: api/notification/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationById(string id)
        {
            var query = new GetNotificationByIdQuery { NotificationId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound(new { message = "Notification not found" });
            }

            return Ok(result);
        }

        // GET: api/notification/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserNotifications(
            string userId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var query = new GetUserNotificationsQuery
            {
                UserId = userId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/notification/user/{userId}/unread
        [HttpGet("user/{userId}/unread")]
        public async Task<IActionResult> GetUnreadNotifications(
            string userId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var query = new GetUnreadNotificationsQuery
            {
                UserId = userId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/notification/user/{userId}/unread-count
        [HttpGet("user/{userId}/unread-count")]
        public async Task<IActionResult> GetUnreadCount(string userId)
        {
            var query = new GetUnreadCountQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return Ok(new { unreadCount = result });
        }

        // PUT: api/notification/{id}/read
        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkAsRead(string id)
        {
            var command = new MarkNotificationAsReadCommand { NotificationId = id };
            var result = await _mediator.Send(command);
            return Ok(new { success = result, message = "Notification marked as read" });
        }

        // PUT: api/notification/user/{userId}/read-all
        [HttpPut("user/{userId}/read-all")]
        public async Task<IActionResult> MarkAllAsRead(string userId)
        {
            var command = new MarkAllNotificationsAsReadCommand { UserId = userId };
            var result = await _mediator.Send(command);
            return Ok(new { success = result, message = "All notifications marked as read" });
        }

        // DELETE: api/notification/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(string id)
        {
            var command = new DeleteNotificationCommand { NotificationId = id };
            var result = await _mediator.Send(command);
            return Ok(new { success = result, message = "Notification deleted" });
        }
    }
}
