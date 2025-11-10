using Domain.Classes;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> GetByIdAsync(string notificationId);
        Task<IEnumerable<Notification>> GetByUserIdAsync(string userId, int pageNumber = 1, int pageSize = 20);
        Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(string userId);
        Task<int> GetUnreadCountAsync(string userId);
        Task<Notification> AddAsync(Notification notification);
        Task MarkAsReadAsync(string notificationId);
        Task MarkAllAsReadAsync(string userId);
        Task DeleteAsync(string notificationId);
        Task DeleteExpiredNotificationsAsync();
    }
}
