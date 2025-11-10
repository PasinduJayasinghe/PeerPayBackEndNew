using Application.Commands.NotificationCommand;
using Application.Dtos;
using Application.Interfaces;
using Domain.Classes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.NotificationCommand
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, NotificationDto>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;

        public CreateNotificationCommandHandler(
            INotificationRepository notificationRepository,
            IUserRepository userRepository)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }

        public async Task<NotificationDto> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            // Verify user exists
            var userExists = await _userRepository.ExistsAsync(request.UserId);
            if (!userExists)
            {
                throw new Exception("User not found");
            }

            var notification = new Notification
            {
                UserId = request.UserId,
                Title = request.Title,
                Content = request.Content,
                Type = request.Type,
                ActionUrl = request.ActionUrl,
                Metadata = request.Metadata,
                ExpiresAt = request.ExpiresAt
            };

            var createdNotification = await _notificationRepository.AddAsync(notification);

            return new NotificationDto
            {
                NotificationId = createdNotification.NotificationId,
                UserId = createdNotification.UserId,
                Title = createdNotification.Title,
                Content = createdNotification.Content,
                Type = createdNotification.Type,
                IsRead = createdNotification.IsRead,
                CreatedAt = createdNotification.CreatedAt,
                ReadAt = createdNotification.ReadAt,
                ActionUrl = createdNotification.ActionUrl,
                ExpiresAt = createdNotification.ExpiresAt
            };
        }
    }

    public class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand, bool>
    {
        private readonly INotificationRepository _notificationRepository;

        public MarkNotificationAsReadCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<bool> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepository.GetByIdAsync(request.NotificationId);
            if (notification == null)
            {
                throw new Exception("Notification not found");
            }

            await _notificationRepository.MarkAsReadAsync(request.NotificationId);
            return true;
        }
    }

    public class MarkAllNotificationsAsReadCommandHandler : IRequestHandler<MarkAllNotificationsAsReadCommand, bool>
    {
        private readonly INotificationRepository _notificationRepository;

        public MarkAllNotificationsAsReadCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<bool> Handle(MarkAllNotificationsAsReadCommand request, CancellationToken cancellationToken)
        {
            await _notificationRepository.MarkAllAsReadAsync(request.UserId);
            return true;
        }
    }

    public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand, bool>
    {
        private readonly INotificationRepository _notificationRepository;

        public DeleteNotificationCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<bool> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepository.GetByIdAsync(request.NotificationId);
            if (notification == null)
            {
                throw new Exception("Notification not found");
            }

            await _notificationRepository.DeleteAsync(request.NotificationId);
            return true;
        }
    }
}
