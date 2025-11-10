using Application.Interfaces;
using Domain.Classes;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface INotificationService
    {
        Task NotifyJobApplicationAsync(string employerId, string studentId, string jobId, string jobTitle);
        Task NotifyApplicationStatusChangeAsync(string studentId, string jobId, string jobTitle, ApplicationStatus status);
        Task NotifyPaymentReceivedAsync(string userId, decimal amount, string jobTitle);
        Task NotifyPaymentSentAsync(string userId, decimal amount, string jobTitle);
        Task NotifyNewMessageAsync(string recipientId, string senderId, string senderName);
        Task NotifyJobAcceptedAsync(string employerId, string studentId, string jobId, string jobTitle);
    }

    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task NotifyJobApplicationAsync(string employerId, string studentId, string jobId, string jobTitle)
        {
            var notification = new Notification
            {
                NotificationId = Guid.NewGuid().ToString(),
                UserId = employerId,
                Title = "New Job Application",
                Content = $"You have received a new application for your job: {jobTitle}",
                Type = NotificationType.JobApplication,
                ActionUrl = $"/jobs/{jobId}/applications",
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(30)
            };

            await _notificationRepository.AddAsync(notification);
        }

        public async Task NotifyApplicationStatusChangeAsync(string studentId, string jobId, string jobTitle, ApplicationStatus status)
        {
            var statusText = status switch
            {
                ApplicationStatus.Selected => "accepted",
                ApplicationStatus.Rejected => "rejected",
                ApplicationStatus.Submitted => "marked as completed",
                _ => "updated"
            };

            var notification = new Notification
            {
                NotificationId = Guid.NewGuid().ToString(),
                UserId = studentId,
                Title = "Application Status Update",
                Content = $"Your application for '{jobTitle}' has been {statusText}",
                Type = NotificationType.JobStatus,
                ActionUrl = $"/jobs/{jobId}",
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(30)
            };

            await _notificationRepository.AddAsync(notification);
        }

        public async Task NotifyPaymentReceivedAsync(string userId, decimal amount, string jobTitle)
        {
            var notification = new Notification
            {
                NotificationId = Guid.NewGuid().ToString(),
                UserId = userId,
                Title = "Payment Received",
                Content = $"You have received ${amount:F2} for completing '{jobTitle}'",
                Type = NotificationType.Payment,
                ActionUrl = "/payments/history",
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(60)
            };

            await _notificationRepository.AddAsync(notification);
        }

        public async Task NotifyPaymentSentAsync(string userId, decimal amount, string jobTitle)
        {
            var notification = new Notification
            {
                NotificationId = Guid.NewGuid().ToString(),
                UserId = userId,
                Title = "Payment Sent",
                Content = $"Your payment of ${amount:F2} for '{jobTitle}' has been processed",
                Type = NotificationType.Payment,
                ActionUrl = "/payments/history",
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(60)
            };

            await _notificationRepository.AddAsync(notification);
        }

        public async Task NotifyNewMessageAsync(string recipientId, string senderId, string senderName)
        {
            var notification = new Notification
            {
                NotificationId = Guid.NewGuid().ToString(),
                UserId = recipientId,
                Title = "New Message",
                Content = $"You have received a new message from {senderName}",
                Type = NotificationType.Message,
                ActionUrl = $"/messages/{senderId}",
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(14)
            };

            await _notificationRepository.AddAsync(notification);
        }

        public async Task NotifyJobAcceptedAsync(string employerId, string studentId, string jobId, string jobTitle)
        {
            var notification = new Notification
            {
                NotificationId = Guid.NewGuid().ToString(),
                UserId = employerId,
                Title = "Job Accepted",
                Content = $"Your job '{jobTitle}' has been accepted by a student",
                Type = NotificationType.JobStatus,
                ActionUrl = $"/jobs/{jobId}",
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(30)
            };

            await _notificationRepository.AddAsync(notification);
        }
    }
}
