using Domain.Classes;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string userId);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByPhoneAsync(string phone);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> PhoneExistsAsync(string phone);
        Task<bool> ExistsAsync(string userId);
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> GetByStatusAsync(UserStatus status);
        Task<IEnumerable<User>> GetByUserTypeAsync(UserType userType);
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string userId);
        Task UpdateLastLoginAsync(string userId);
    }
}
