using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UserDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public UserType UserType { get; set; }
        public UserStatus Status { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public ProfileDto Profile { get; set; }
    }

    public class ProfileDto
    {
        public string ProfileId { get; set; }
        public string Bio { get; set; }
        public string Address { get; set; }
        public string ProfilePictureUrl { get; set; }
    }

    public class UpdateUserProfileDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Address { get; set; }
    }

    public class ChangePasswordDto
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }

    public class UpdateUserStatusDto
    {
        public string UserId { get; set; }
        public UserStatus Status { get; set; }
    }

    public class VerifyUserDto
    {
        public string UserId { get; set; }
        public bool IsVerified { get; set; }
    }
}
