using Application.Commands.UserCommand;
using Application.Interfaces;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserCommand
{
    public class UpdateUserStatusCommandHandler : IRequestHandler<UpdateUserStatusCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserStatusCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Status = request.Status;

            await _userRepository.UpdateAsync(user);

            return true;
        }
    }

    public class VerifyUserCommandHandler : IRequestHandler<VerifyUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public VerifyUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(VerifyUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.IsVerified = request.IsVerified;

            // If verifying user, also update status to Active
            if (request.IsVerified && user.Status == UserStatus.PendingVerification)
            {
                user.Status = UserStatus.Active;
            }

            await _userRepository.UpdateAsync(user);

            return true;
        }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            await _userRepository.DeleteAsync(request.UserId);

            return true;
        }
    }
}
