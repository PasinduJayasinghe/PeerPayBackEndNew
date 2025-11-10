using Application.Dtos;
using Application.Interfaces;
using Application.Queries.NotificationQuery;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.NotificationQuery
{
    public class GetUserNotificationsQueryHandler : IRequestHandler<GetUserNotificationsQuery, NotificationListDto>
    {
        private readonly INotificationRepository _notificationRepository;

        public GetUserNotificationsQueryHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<NotificationListDto> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _notificationRepository.GetByUserIdAsync(
                request.UserId,
                request.PageNumber,
                request.PageSize
            );

            var unreadCount = await _notificationRepository.GetUnreadCountAsync(request.UserId);

            var notificationDtos = notifications.Select(n => new NotificationDto
            {
                NotificationId = n.NotificationId,
                UserId = n.UserId,
                Title = n.Title,
                Content = n.Content,
                Type = n.Type,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt,
                ReadAt = n.ReadAt,
                ActionUrl = n.ActionUrl,
                ExpiresAt = n.ExpiresAt
            }).ToList();

            return new NotificationListDto
            {
                Notifications = notificationDtos,
                UnreadCount = unreadCount,
                TotalCount = notificationDtos.Count,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }

    public class GetUnreadNotificationsQueryHandler : IRequestHandler<GetUnreadNotificationsQuery, NotificationListDto>
    {
        private readonly INotificationRepository _notificationRepository;

        public GetUnreadNotificationsQueryHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<NotificationListDto> Handle(GetUnreadNotificationsQuery request, CancellationToken cancellationToken)
        {
            var unreadNotifications = await _notificationRepository.GetUnreadNotificationsAsync(request.UserId);

            var notificationDtos = unreadNotifications.Select(n => new NotificationDto
            {
                NotificationId = n.NotificationId,
                UserId = n.UserId,
                Title = n.Title,
                Content = n.Content,
                Type = n.Type,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt,
                ReadAt = n.ReadAt,
                ActionUrl = n.ActionUrl,
                ExpiresAt = n.ExpiresAt
            }).ToList();

            return new NotificationListDto
            {
                Notifications = notificationDtos,
                UnreadCount = notificationDtos.Count,
                TotalCount = notificationDtos.Count,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }

    public class GetUnreadCountQueryHandler : IRequestHandler<GetUnreadCountQuery, int>
    {
        private readonly INotificationRepository _notificationRepository;

        public GetUnreadCountQueryHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<int> Handle(GetUnreadCountQuery request, CancellationToken cancellationToken)
        {
            return await _notificationRepository.GetUnreadCountAsync(request.UserId);
        }
    }

    public class GetNotificationByIdQueryHandler : IRequestHandler<GetNotificationByIdQuery, NotificationDto?>
    {
        private readonly INotificationRepository _notificationRepository;

        public GetNotificationByIdQueryHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<NotificationDto?> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepository.GetByIdAsync(request.NotificationId);
            if (notification == null)
            {
                return null;
            }

            return new NotificationDto
            {
                NotificationId = notification.NotificationId,
                UserId = notification.UserId,
                Title = notification.Title,
                Content = notification.Content,
                Type = notification.Type,
                IsRead = notification.IsRead,
                CreatedAt = notification.CreatedAt,
                ReadAt = notification.ReadAt,
                ActionUrl = notification.ActionUrl,
                ExpiresAt = notification.ExpiresAt
            };
        }
    }
}
