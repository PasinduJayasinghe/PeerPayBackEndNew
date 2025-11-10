using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message> GetByIdAsync(string messageId);
        Task<IEnumerable<Message>> GetByConversationIdAsync(string conversationId, int pageNumber = 1, int pageSize = 50);
        Task<IEnumerable<Message>> GetUnreadMessagesAsync(string userId);
        Task<int> GetUnreadCountAsync(string conversationId, string userId);
        Task<Message> AddAsync(Message message);
        Task MarkAsReadAsync(string messageId);
        Task MarkConversationAsReadAsync(string conversationId, string userId);
        Task DeleteAsync(string messageId);
        Task<IEnumerable<Message>> SearchMessagesAsync(string conversationId, string searchTerm);
    }

    public interface IConversationRepository
    {
        Task<Conversation> GetByIdAsync(string conversationId);
        Task<IEnumerable<Conversation>> GetByUserIdAsync(string userId);
        Task<Conversation> GetByParticipantsAsync(string userId1, string userId2, string jobId);
        Task<Conversation> AddAsync(Conversation conversation);
        Task UpdateAsync(Conversation conversation);
        Task DeleteAsync(string conversationId);
        Task<bool> ExistsAsync(string conversationId);
    }
}
