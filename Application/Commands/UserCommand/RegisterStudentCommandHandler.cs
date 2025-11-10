using Application.Commands.StudentCommand;
using Application.Dtos;
using Application.Interfaces;
using Domain.Classes;
using Domain.Enums;
using Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserCommand
{
    public class RegisterStudentCommandHandler : IRequestHandler<RegisterStudentCommand, UserResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMediator _mediator;

        public RegisterStudentCommandHandler(
            IUserRepository userRepository,
            IStudentRepository studentRepository,
            IPasswordHasher passwordHasher,
            IMediator mediator)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _passwordHasher = passwordHasher;
            _mediator = mediator;
        }

        public async Task<UserResponseDto> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
        {
            // Check if email already exists
            if (await _userRepository.EmailExistsAsync(request.Email))
            {
                throw new Exception("Email already exists");
            }

            // Check if phone already exists
            if (await _userRepository.PhoneExistsAsync(request.Phone))
            {
                throw new Exception("Phone number already exists");
            }

            // Create User entity
            var user = new User
            {
                UserId = Guid.NewGuid().ToString(),
                Email = request.Email.ToLower(),
                Phone = NormalizePhoneNumber(request.Phone),
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                Name = request.Name,
                UserType = UserType.Student,
                Status = UserStatus.PendingVerification,
                IsVerified = false,
                CreatedAt = DateTime.UtcNow
            };

            // Save user
            var createdUser = await _userRepository.AddAsync(user);

            // Create Student entity
            var student = new Student
            {
                StudentId = Guid.NewGuid().ToString(),
                UserId = createdUser.UserId,
                University = request.University,
                Course = request.Course,
                YearOfStudy = request.YearOfStudy,
                AcademicVerificationStatus = "Pending",
                Rating = 0,
                CompletedJobs = 0,
                TotalEarnings = 0
            };

            // Save student
            await _studentRepository.AddAsync(student);

            // Publish domain event
            await _mediator.Publish(new UserRegisteredEvent
            {
                UserId = createdUser.UserId,
                Email = createdUser.Email,
                UserType = UserType.Student.ToString()
            }, cancellationToken);

            // Return response
            return new UserResponseDto
            {
                UserId = createdUser.UserId,
                Email = createdUser.Email,
                Phone = createdUser.Phone,
                Name = createdUser.Name,
                UserType = createdUser.UserType,
                Status = createdUser.Status,
                IsVerified = createdUser.IsVerified,
                CreatedAt = createdUser.CreatedAt
            };
        }

        private string NormalizePhoneNumber(string phone)
        {
            // Convert 0XXXXXXXXX to +94XXXXXXXXX
            if (phone.StartsWith("0"))
            {
                return "+94" + phone.Substring(1);
            }
            return phone;
        }
    }
}
