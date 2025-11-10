using Application.Interfaces;
using Domain.Classes;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly PeerPayDbContext _context;

        public MessageRepository(PeerPayDbContext context)
        {
            _context = context;
        }

        public async Task<Message> GetByIdAsync(string messageId)
        {
            return await _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Conversation)
                .FirstOrDefaultAsync(m => m.MessageId == messageId);
        }

        public async Task<IEnumerable<Message>> GetByConversationIdAsync(string conversationId, int pageNumber = 1, int pageSize = 50)
        {
            return await _context.Messages
                .Where(m => m.ConversationId == conversationId)
                .OrderByDescending(m => m.Timestamp)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(m => m.Sender)
                .ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetUnreadMessagesAsync(string userId)
        {
            return await _context.Messages
                .Include(m => m.Conversation)
                .Include(m => m.Sender)
                .Where(m => !m.IsRead && 
                           (m.Conversation.Participant1Id == userId || m.Conversation.Participant2Id == userId) &&
                           m.SenderId != userId)
                .OrderByDescending(m => m.Timestamp)
                .ToListAsync();
        }

        public async Task<int> GetUnreadCountAsync(string conversationId, string userId)
        {
            return await _context.Messages
                .CountAsync(m => m.ConversationId == conversationId && 
                                !m.IsRead && 
                                m.SenderId != userId);
        }

        public async Task<Message> AddAsync(Message message)
        {
            message.Timestamp = DateTime.UtcNow;
            message.Status = MessageStatus.Sent;
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task MarkAsReadAsync(string messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message != null && !message.IsRead)
            {
                message.IsRead = true;
                message.ReadAt = DateTime.UtcNow;
                message.Status = MessageStatus.Read;
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkConversationAsReadAsync(string conversationId, string userId)
        {
            var unreadMessages = await _context.Messages
                .Where(m => m.ConversationId == conversationId && 
                           !m.IsRead && 
                           m.SenderId != userId)
                .ToListAsync();

            foreach (var message in unreadMessages)
            {
                message.IsRead = true;
                message.ReadAt = DateTime.UtcNow;
                message.Status = MessageStatus.Read;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message != null)
            {
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Message>> SearchMessagesAsync(string conversationId, string searchTerm)
        {
            return await _context.Messages
                .Where(m => m.ConversationId == conversationId && 
                           m.Content.Contains(searchTerm))
                .OrderByDescending(m => m.Timestamp)
                .Include(m => m.Sender)
                .ToListAsync();
        }
    }

    public class ConversationRepository : IConversationRepository
    {
        private readonly PeerPayDbContext _context;

        public ConversationRepository(PeerPayDbContext context)
        {
            _context = context;
        }

        public async Task<Conversation> GetByIdAsync(string conversationId)
        {
            return await _context.Conversations
                .Include(c => c.Participant1)
                .Include(c => c.Participant2)
                .Include(c => c.Job)
                .Include(c => c.Messages.OrderByDescending(m => m.Timestamp).Take(1))
                .FirstOrDefaultAsync(c => c.ConversationId == conversationId);
        }

        public async Task<IEnumerable<Conversation>> GetByUserIdAsync(string userId)
        {
            return await _context.Conversations
                .Where(c => (c.Participant1Id == userId || c.Participant2Id == userId) && c.IsActive)
                .Include(c => c.Participant1)
                .Include(c => c.Participant2)
                .Include(c => c.Job)
                .Include(c => c.Messages.OrderByDescending(m => m.Timestamp).Take(1))
                .OrderByDescending(c => c.LastMessageAt)
                .ToListAsync();
        }

        public async Task<Conversation> GetByParticipantsAsync(string userId1, string userId2, string jobId)
        {
            return await _context.Conversations
                .FirstOrDefaultAsync(c => c.JobId == jobId &&
                    ((c.Participant1Id == userId1 && c.Participant2Id == userId2) ||
                     (c.Participant1Id == userId2 && c.Participant2Id == userId1)));
        }

        public async Task<Conversation> AddAsync(Conversation conversation)
        {
            conversation.LastMessageAt = DateTime.UtcNow;
            conversation.IsActive = true;
            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync();
            return conversation;
        }

        public async Task UpdateAsync(Conversation conversation)
        {
            _context.Conversations.Update(conversation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string conversationId)
        {
            var conversation = await _context.Conversations.FindAsync(conversationId);
            if (conversation != null)
            {
                conversation.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string conversationId)
        {
            return await _context.Conversations.AnyAsync(c => c.ConversationId == conversationId);
        }
    }
}
