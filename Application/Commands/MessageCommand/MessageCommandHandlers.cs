using Application.Commands.MessageCommand;
using Application.Commands.ConversationCommand;
using Application.Dtos;
using Application.Interfaces;
using Domain.Classes;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;

namespace Application.Commands.MessageCommand
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, MessageDto>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IConversationRepository _conversationRepository;
        private readonly INotificationService _notificationService;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<SendMessageCommandHandler> _logger;

        public SendMessageCommandHandler(
            IMessageRepository messageRepository,
            IConversationRepository conversationRepository,
            INotificationService notificationService,
            IUserRepository userRepository,
            ILogger<SendMessageCommandHandler> logger)
        {
            _messageRepository = messageRepository;
            _conversationRepository = conversationRepository;
            _notificationService = notificationService;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<MessageDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing message from sender {SenderId} in conversation {ConversationId}", 
                request.SenderId, request.ConversationId);
            
            var conversation = await _conversationRepository.GetByIdAsync(request.ConversationId);
            if (conversation == null)
            {
                _logger.LogWarning("Message failed: Conversation {ConversationId} not found", request.ConversationId);
                throw new Exception("Conversation not found");
            }

            var message = new Message
            {
                MessageId = Guid.NewGuid().ToString(),
                ConversationId = request.ConversationId,
                SenderId = request.SenderId,
                Content = request.Content,
                Attachments = request.Attachments ?? Array.Empty<string>()
            };

            var createdMessage = await _messageRepository.AddAsync(message);

            // Update conversation last message time
            conversation.LastMessageAt = DateTime.UtcNow;
            await _conversationRepository.UpdateAsync(conversation);

            // Send notification to recipient
            var recipientId = conversation.Participant1Id == request.SenderId 
                ? conversation.Participant2Id 
                : conversation.Participant1Id;
            
            var sender = await _userRepository.GetByIdAsync(request.SenderId);
            await _notificationService.NotifyNewMessageAsync(recipientId, request.SenderId, sender.Name);

            return new MessageDto
            {
                MessageId = createdMessage.MessageId,
                ConversationId = createdMessage.ConversationId,
                SenderId = createdMessage.SenderId,
                SenderName = sender.Name,
                Content = createdMessage.Content,
                Attachments = createdMessage.Attachments,
                Timestamp = createdMessage.Timestamp,
                Status = createdMessage.Status,
                IsRead = createdMessage.IsRead,
                ReadAt = createdMessage.ReadAt
            };
        }
    }

    public class MarkMessageAsReadCommandHandler : IRequestHandler<MarkMessageAsReadCommand, bool>
    {
        private readonly IMessageRepository _messageRepository;

        public MarkMessageAsReadCommandHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<bool> Handle(MarkMessageAsReadCommand request, CancellationToken cancellationToken)
        {
            var message = await _messageRepository.GetByIdAsync(request.MessageId);
            if (message == null)
            {
                throw new Exception("Message not found");
            }

            await _messageRepository.MarkAsReadAsync(request.MessageId);
            return true;
        }
    }

    public class MarkConversationAsReadCommandHandler : IRequestHandler<MarkConversationAsReadCommand, bool>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IConversationRepository _conversationRepository;

        public MarkConversationAsReadCommandHandler(
            IMessageRepository messageRepository,
            IConversationRepository conversationRepository)
        {
            _messageRepository = messageRepository;
            _conversationRepository = conversationRepository;
        }

        public async Task<bool> Handle(MarkConversationAsReadCommand request, CancellationToken cancellationToken)
        {
            var conversation = await _conversationRepository.GetByIdAsync(request.ConversationId);
            if (conversation == null)
            {
                throw new Exception("Conversation not found");
            }

            await _messageRepository.MarkConversationAsReadAsync(request.ConversationId, request.UserId);
            return true;
        }
    }

    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, bool>
    {
        private readonly IMessageRepository _messageRepository;

        public DeleteMessageCommandHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _messageRepository.GetByIdAsync(request.MessageId);
            if (message == null)
            {
                throw new Exception("Message not found");
            }

            await _messageRepository.DeleteAsync(request.MessageId);
            return true;
        }
    }
}

namespace Application.Commands.ConversationCommand
{
    public class CreateConversationCommandHandler : IRequestHandler<CreateConversationCommand, ConversationDto>
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJobRepository _jobRepository;

        public CreateConversationCommandHandler(
            IConversationRepository conversationRepository,
            IUserRepository userRepository,
            IJobRepository jobRepository)
        {
            _conversationRepository = conversationRepository;
            _userRepository = userRepository;
            _jobRepository = jobRepository;
        }

        public async Task<ConversationDto> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
        {
            // Check if conversation already exists
            var existingConversation = await _conversationRepository.GetByParticipantsAsync(
                request.Participant1Id, 
                request.Participant2Id, 
                request.JobId);

            if (existingConversation != null)
            {
                var user1 = await _userRepository.GetByIdAsync(existingConversation.Participant1Id);
                var user2 = await _userRepository.GetByIdAsync(existingConversation.Participant2Id);
                var existingJob = await _jobRepository.GetByIdAsync(existingConversation.JobId);

                return new ConversationDto
                {
                    ConversationId = existingConversation.ConversationId,
                    Participant1Id = existingConversation.Participant1Id,
                    Participant1Name = user1.Name,
                    Participant2Id = existingConversation.Participant2Id,
                    Participant2Name = user2.Name,
                    JobId = existingConversation.JobId,
                    JobTitle = existingJob.Title,
                    LastMessageAt = existingConversation.LastMessageAt,
                    IsActive = existingConversation.IsActive
                };
            }

            // Verify users and job exist
            var participant1 = await _userRepository.GetByIdAsync(request.Participant1Id);
            var participant2 = await _userRepository.GetByIdAsync(request.Participant2Id);
            var job = await _jobRepository.GetByIdAsync(request.JobId);

            if (participant1 == null || participant2 == null)
            {
                throw new Exception("One or both participants not found");
            }

            if (job == null)
            {
                throw new Exception("Job not found");
            }

            var conversation = new Conversation
            {
                ConversationId = Guid.NewGuid().ToString(),
                Participant1Id = request.Participant1Id,
                Participant2Id = request.Participant2Id,
                JobId = request.JobId
            };

            var createdConversation = await _conversationRepository.AddAsync(conversation);

            return new ConversationDto
            {
                ConversationId = createdConversation.ConversationId,
                Participant1Id = createdConversation.Participant1Id,
                Participant1Name = participant1.Name,
                Participant2Id = createdConversation.Participant2Id,
                Participant2Name = participant2.Name,
                JobId = createdConversation.JobId,
                JobTitle = job.Title,
                LastMessageAt = createdConversation.LastMessageAt,
                IsActive = createdConversation.IsActive
            };
        }
    }
}
