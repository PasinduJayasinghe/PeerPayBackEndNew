using Application.Dtos;
using Application.Interfaces;
using Domain.Classes;
using Domain.Enums;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EmployerCommand
{
    public class RegisterEmployerCommandHandler : IRequestHandler<RegisterEmployerCommand, UserResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmployerRepository _employerRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMediator _mediator;
        private readonly ILogger<RegisterEmployerCommandHandler> _logger;

        public RegisterEmployerCommandHandler(
            IUserRepository userRepository,
            IEmployerRepository employerRepository,
            IPasswordHasher passwordHasher,
            IMediator mediator,
            ILogger<RegisterEmployerCommandHandler> logger)
        {
            _userRepository = userRepository;
            _employerRepository = employerRepository;
            _passwordHasher = passwordHasher;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<UserResponseDto> Handle(RegisterEmployerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing employer registration for email: {Email}", request.Email);
            
            // Check if email already exists
            if (await _userRepository.EmailExistsAsync(request.Email))
            {
                _logger.LogWarning("Registration failed: Email {Email} already exists", request.Email);
                throw new Exception("Email already exists");
            }

            // Check if phone already exists
            if (await _userRepository.PhoneExistsAsync(request.Phone))
            {
                _logger.LogWarning("Registration failed: Phone {Phone} already exists", request.Phone);
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
                UserType = UserType.Employer,
                Status = UserStatus.PendingVerification,
                IsVerified = false,
                CreatedAt = DateTime.UtcNow
            };

            // Save user
            var createdUser = await _userRepository.AddAsync(user);

            // Create Employer entity
            var employer = new Employer
            {
                EmployerId = Guid.NewGuid().ToString(),
                UserId = createdUser.UserId,
                CompanyName = request.CompanyName,
                CompanyType = request.CompanyType,
                Description = request.Description,
                ContactPerson = request.ContactPerson,
                VerificationStatus = "Pending",
                Rating = 0,
                JobsPosted = 0
            };

            // Save employer
            await _employerRepository.AddAsync(employer);

            // Publish domain event
            await _mediator.Publish(new UserRegisteredEvent
            {
                UserId = createdUser.UserId,
                Email = createdUser.Email,
                UserType = UserType.Employer.ToString()
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
            if (phone.StartsWith("0"))
            {
                return "+94" + phone.Substring(1);
            }
            return phone;
        }
    }
}
