using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public UserType UserType { get; set; }
    }

    public class RegisterStudentDto : RegisterUserDto
    {
        public string University { get; set; }
        public string Course { get; set; }
        public int YearOfStudy { get; set; }
    }

    public class RegisterEmployerDto : RegisterUserDto
    {
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public string Description { get; set; }
        public string ContactPerson { get; set; }
    }

    public class UserResponseDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public UserType UserType { get; set; }
        public UserStatus Status { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class LoginUserDto
    {
        public string EmailOrPhone { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
        public UserType UserType { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
