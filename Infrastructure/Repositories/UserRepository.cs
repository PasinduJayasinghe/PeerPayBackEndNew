using Application.Interfaces;
using Domain.Classes;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PeerPayDbContext _context;

        public UserRepository(PeerPayDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            return await _context.Users
                .Include(u => u.Profile)
                .Include(u => u.Notifications)
                .Include(u => u.Sessions)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByPhoneAsync(string phone)
        {
            return await _context.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Phone == phone);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> PhoneExistsAsync(string phone)
        {
            return await _context.Users.AnyAsync(u => u.Phone == phone);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Profile)
                .ToListAsync();
        }

        public async Task<User> AddAsync(User user)
        {
            user.UserId = Guid.NewGuid().ToString();
            user.CreatedAt = DateTime.UtcNow;
            user.Status = UserStatus.PendingVerification;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string userId)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId);
        }

        public async Task UpdateLastLoginAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.LastLogin = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetByStatusAsync(UserStatus status)
        {
            return await _context.Users
                .Where(u => u.Status == status)
                .Include(u => u.Profile)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetByUserTypeAsync(UserType userType)
        {
            return await _context.Users
                .Where(u => u.UserType == userType)
                .Include(u => u.Profile)
                .ToListAsync();
        }
    }
}
