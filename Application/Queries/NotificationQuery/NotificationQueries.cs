using Application.Dtos;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.NotificationQuery
{
    public class GetUserNotificationsQuery : IRequest<NotificationListDto>
    {
        public string UserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class GetUnreadNotificationsQuery : IRequest<NotificationListDto>
    {
        public string UserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class GetUnreadCountQuery : IRequest<int>
    {
        public string UserId { get; set; }
    }

    public class GetNotificationByIdQuery : IRequest<NotificationDto?>
    {
        public string NotificationId { get; set; }
    }
}
