using Application.Commands.UserCommand;
using Application.Dtos;
using Application.Interfaces;
using Domain.Classes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserCommand
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserProfileCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Update user name
            user.Name = request.Name;

            // Update or create profile
            if (user.Profile == null)
            {
                user.Profile = new Profile
                {
                    ProfileId = Guid.NewGuid().ToString(),
                    UserId = user.UserId,
                    Bio = request.Bio,
                    Address = request.Address
                };
            }
            else
            {
                user.Profile.Bio = request.Bio;
                user.Profile.Address = request.Address;
            }

            await _userRepository.UpdateAsync(user);

            return new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Phone = user.Phone,
                Name = user.Name,
                UserType = user.UserType,
                Status = user.Status,
                IsVerified = user.IsVerified,
                CreatedAt = user.CreatedAt,
                LastLogin = user.LastLogin,
                Profile = new ProfileDto
                {
                    ProfileId = user.Profile.ProfileId,
                    Bio = user.Profile.Bio,
                    Address = user.Profile.Address,
                    ProfilePictureUrl = user.Profile.ProfilePictureUrl
                }
            };
        }
    }
}
