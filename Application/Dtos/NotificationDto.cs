using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class NotificationDto
    {
        public string NotificationId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ReadAt { get; set; }
        public string ActionUrl { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }

    public class CreateNotificationDto
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public NotificationType Type { get; set; }
        public string ActionUrl { get; set; }
        public string Metadata { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }

    public class NotificationListDto
    {
        public IEnumerable<NotificationDto> Notifications { get; set; }
        public int UnreadCount { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
