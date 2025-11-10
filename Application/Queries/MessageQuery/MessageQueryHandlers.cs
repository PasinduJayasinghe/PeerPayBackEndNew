using Application.Dtos;
using Application.Interfaces;
using Application.Queries.MessageQuery;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.MessageQuery
{
    public class GetConversationMessagesQueryHandler : IRequestHandler<GetConversationMessagesQuery, MessageListDto>
    {
        private readonly IMessageRepository _messageRepository;

        public GetConversationMessagesQueryHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<MessageListDto> Handle(GetConversationMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.GetByConversationIdAsync(
                request.ConversationId,
                request.PageNumber,
                request.PageSize
            );

            var unreadCount = await _messageRepository.GetUnreadCountAsync(request.ConversationId, request.UserId);

            var messageDtos = messages.Select(m => new MessageDto
            {
                MessageId = m.MessageId,
                ConversationId = m.ConversationId,
                SenderId = m.SenderId,
                SenderName = m.Sender?.Name ?? "Unknown",
                Content = m.Content,
                Attachments = m.Attachments,
                Timestamp = m.Timestamp,
                Status = m.Status,
                IsRead = m.IsRead,
                ReadAt = m.ReadAt
            }).ToList();

            return new MessageListDto
            {
                Messages = messageDtos,
                TotalCount = messageDtos.Count,
                UnreadCount = unreadCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }

    public class GetUnreadMessagesQueryHandler : IRequestHandler<GetUnreadMessagesQuery, IEnumerable<MessageDto>>
    {
        private readonly IMessageRepository _messageRepository;

        public GetUnreadMessagesQueryHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<IEnumerable<MessageDto>> Handle(GetUnreadMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.GetUnreadMessagesAsync(request.UserId);

            return messages.Select(m => new MessageDto
            {
                MessageId = m.MessageId,
                ConversationId = m.ConversationId,
                SenderId = m.SenderId,
                SenderName = m.Sender?.Name ?? "Unknown",
                Content = m.Content,
                Attachments = m.Attachments,
                Timestamp = m.Timestamp,
                Status = m.Status,
                IsRead = m.IsRead,
                ReadAt = m.ReadAt
            }).ToList();
        }
    }

    public class SearchMessagesQueryHandler : IRequestHandler<SearchMessagesQuery, IEnumerable<MessageDto>>
    {
        private readonly IMessageRepository _messageRepository;

        public SearchMessagesQueryHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<IEnumerable<MessageDto>> Handle(SearchMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.SearchMessagesAsync(request.ConversationId, request.SearchTerm);

            return messages.Select(m => new MessageDto
            {
                MessageId = m.MessageId,
                ConversationId = m.ConversationId,
                SenderId = m.SenderId,
                SenderName = m.Sender?.Name ?? "Unknown",
                Content = m.Content,
                Attachments = m.Attachments,
                Timestamp = m.Timestamp,
                Status = m.Status,
                IsRead = m.IsRead,
                ReadAt = m.ReadAt
            }).ToList();
        }
    }

    public class GetUserConversationsQueryHandler : IRequestHandler<GetUserConversationsQuery, IEnumerable<ConversationDto>>
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMessageRepository _messageRepository;

        public GetUserConversationsQueryHandler(
            IConversationRepository conversationRepository,
            IMessageRepository messageRepository)
        {
            _conversationRepository = conversationRepository;
            _messageRepository = messageRepository;
        }

        public async Task<IEnumerable<ConversationDto>> Handle(GetUserConversationsQuery request, CancellationToken cancellationToken)
        {
            var conversations = await _conversationRepository.GetByUserIdAsync(request.UserId);

            var conversationDtos = new List<ConversationDto>();

            foreach (var conv in conversations)
            {
                var unreadCount = await _messageRepository.GetUnreadCountAsync(conv.ConversationId, request.UserId);
                var lastMessage = conv.Messages?.FirstOrDefault();

                conversationDtos.Add(new ConversationDto
                {
                    ConversationId = conv.ConversationId,
                    Participant1Id = conv.Participant1Id,
                    Participant1Name = conv.Participant1?.Name ?? "Unknown",
                    Participant2Id = conv.Participant2Id,
                    Participant2Name = conv.Participant2?.Name ?? "Unknown",
                    JobId = conv.JobId,
                    JobTitle = conv.Job?.Title ?? "Unknown Job",
                    LastMessageAt = conv.LastMessageAt,
                    IsActive = conv.IsActive,
                    UnreadCount = unreadCount,
                    LastMessage = lastMessage != null ? new MessageDto
                    {
                        MessageId = lastMessage.MessageId,
                        ConversationId = lastMessage.ConversationId,
                        SenderId = lastMessage.SenderId,
                        SenderName = lastMessage.Sender?.Name ?? "Unknown",
                        Content = lastMessage.Content,
                        Timestamp = lastMessage.Timestamp,
                        Status = lastMessage.Status,
                        IsRead = lastMessage.IsRead
                    } : null
                });
            }

            return conversationDtos;
        }
    }

    public class GetConversationByIdQueryHandler : IRequestHandler<GetConversationByIdQuery, ConversationDto?>
    {
        private readonly IConversationRepository _conversationRepository;

        public GetConversationByIdQueryHandler(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }

        public async Task<ConversationDto?> Handle(GetConversationByIdQuery request, CancellationToken cancellationToken)
        {
            var conversation = await _conversationRepository.GetByIdAsync(request.ConversationId);
            if (conversation == null)
            {
                return null;
            }

            return new ConversationDto
            {
                ConversationId = conversation.ConversationId,
                Participant1Id = conversation.Participant1Id,
                Participant1Name = conversation.Participant1?.Name ?? "Unknown",
                Participant2Id = conversation.Participant2Id,
                Participant2Name = conversation.Participant2?.Name ?? "Unknown",
                JobId = conversation.JobId,
                JobTitle = conversation.Job?.Title ?? "Unknown Job",
                LastMessageAt = conversation.LastMessageAt,
                IsActive = conversation.IsActive
            };
        }
    }
}
