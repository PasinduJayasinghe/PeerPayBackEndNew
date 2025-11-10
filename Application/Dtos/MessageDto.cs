using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class MessageDto
    {
        public string MessageId { get; set; }
        public string ConversationId { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string Content { get; set; }
        public string[] Attachments { get; set; }
        public DateTime Timestamp { get; set; }
        public MessageStatus Status { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
    }

    public class SendMessageDto
    {
        public string ConversationId { get; set; }
        public string SenderId { get; set; }
        public string Content { get; set; }
        public string[] Attachments { get; set; }
    }

    public class ConversationDto
    {
        public string ConversationId { get; set; }
        public string Participant1Id { get; set; }
        public string Participant1Name { get; set; }
        public string Participant2Id { get; set; }
        public string Participant2Name { get; set; }
        public string JobId { get; set; }
        public string JobTitle { get; set; }
        public DateTime LastMessageAt { get; set; }
        public bool IsActive { get; set; }
        public MessageDto LastMessage { get; set; }
        public int UnreadCount { get; set; }
    }

    public class CreateConversationDto
    {
        public string Participant1Id { get; set; }
        public string Participant2Id { get; set; }
        public string JobId { get; set; }
    }

    public class MessageListDto
    {
        public IEnumerable<MessageDto> Messages { get; set; }
        public int TotalCount { get; set; }
        public int UnreadCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
