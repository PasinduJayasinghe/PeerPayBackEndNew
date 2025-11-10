using Application.Commands.MessageCommand;
using Application.Commands.ConversationCommand;
using Application.Dtos;
using Application.Queries.MessageQuery;
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
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MessageController> _logger;

        public MessageController(IMediator mediator, ILogger<MessageController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // POST: api/message
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
        {
            try
            {
                _logger.LogInformation("Sending message from user {SenderId} in conversation {ConversationId}", dto.SenderId, dto.ConversationId);
                
                var command = new SendMessageCommand
                {
                    ConversationId = dto.ConversationId,
                    SenderId = dto.SenderId,
                    Content = dto.Content,
                    Attachments = dto.Attachments
                };

                var result = await _mediator.Send(command);
                _logger.LogInformation("Message sent successfully: {MessageId}", result.MessageId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending message from user {SenderId}", dto.SenderId);
                return BadRequest(new { error = ex.Message });
            }
        }

        // GET: api/message/conversation/{conversationId}
        [HttpGet("conversation/{conversationId}")]
        public async Task<IActionResult> GetConversationMessages(
            string conversationId,
            [FromQuery] string userId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 50)
        {
            try
            {
                _logger.LogInformation("Retrieving messages for conversation {ConversationId}, user {UserId}", conversationId, userId);
                
                var query = new GetConversationMessagesQuery
                {
                    ConversationId = conversationId,
                    UserId = userId,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                var result = await _mediator.Send(query);
                _logger.LogInformation("Retrieved {Count} messages for conversation {ConversationId}", result.TotalCount, conversationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving messages for conversation {ConversationId}", conversationId);
                return BadRequest(new { error = ex.Message });
            }
        }

        // GET: api/message/unread/{userId}
        [HttpGet("unread/{userId}")]
        public async Task<IActionResult> GetUnreadMessages(string userId)
        {
            var query = new GetUnreadMessagesQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/message/search
        [HttpGet("search")]
        public async Task<IActionResult> SearchMessages(
            [FromQuery] string conversationId,
            [FromQuery] string searchTerm)
        {
            var query = new SearchMessagesQuery
            {
                ConversationId = conversationId,
                SearchTerm = searchTerm
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // PUT: api/message/{id}/read
        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkMessageAsRead(string id)
        {
            var command = new MarkMessageAsReadCommand { MessageId = id };
            var result = await _mediator.Send(command);
            return Ok(new { success = result, message = "Message marked as read" });
        }

        // PUT: api/message/conversation/{conversationId}/read
        [HttpPut("conversation/{conversationId}/read")]
        public async Task<IActionResult> MarkConversationAsRead(
            string conversationId,
            [FromQuery] string userId)
        {
            var command = new MarkConversationAsReadCommand
            {
                ConversationId = conversationId,
                UserId = userId
            };

            var result = await _mediator.Send(command);
            return Ok(new { success = result, message = "Conversation marked as read" });
        }

        // DELETE: api/message/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(string id)
        {
            var command = new DeleteMessageCommand { MessageId = id };
            var result = await _mediator.Send(command);
            return Ok(new { success = result, message = "Message deleted" });
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ConversationController> _logger;

        public ConversationController(IMediator mediator, ILogger<ConversationController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // POST: api/conversation
        [HttpPost]
        public async Task<IActionResult> CreateConversation([FromBody] CreateConversationDto dto)
        {
            try
            {
                _logger.LogInformation("Creating conversation between {Participant1Id} and {Participant2Id} for job {JobId}", 
                    dto.Participant1Id, dto.Participant2Id, dto.JobId);
                
                var command = new CreateConversationCommand
                {
                    Participant1Id = dto.Participant1Id,
                    Participant2Id = dto.Participant2Id,
                    JobId = dto.JobId
                };

                var result = await _mediator.Send(command);
                _logger.LogInformation("Conversation created successfully: {ConversationId}", result.ConversationId);
                return CreatedAtAction(nameof(GetConversationById), new { id = result.ConversationId }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating conversation between {Participant1Id} and {Participant2Id}", 
                    dto.Participant1Id, dto.Participant2Id);
                return BadRequest(new { error = ex.Message });
            }
        }

        // GET: api/conversation/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConversationById(string id)
        {
            var query = new GetConversationByIdQuery { ConversationId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound(new { message = "Conversation not found" });
            }

            return Ok(result);
        }

        // GET: api/conversation/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserConversations(string userId)
        {
            try
            {
                _logger.LogInformation("Retrieving conversations for user {UserId}", userId);
                var query = new GetUserConversationsQuery { UserId = userId };
                var result = await _mediator.Send(query);
                _logger.LogInformation("Retrieved {Count} conversations for user {UserId}", result.Count(), userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving conversations for user {UserId}", userId);
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
