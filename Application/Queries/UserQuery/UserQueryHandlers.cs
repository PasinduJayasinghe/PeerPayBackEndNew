using Application.Dtos;
using Application.Interfaces;
using MediatR;

namespace Application.Queries.UserQuery
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

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
                Profile = user.Profile != null ? new ProfileDto
                {
                    ProfileId = user.Profile.ProfileId,
                    Bio = user.Profile.Bio,
                    Address = user.Profile.Address,
                    ProfilePictureUrl = user.Profile.ProfilePictureUrl
                } : null
            };
        }
    }

    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

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
                Profile = user.Profile != null ? new ProfileDto
                {
                    ProfileId = user.Profile.ProfileId,
                    Bio = user.Profile.Bio,
                    Address = user.Profile.Address,
                    ProfilePictureUrl = user.Profile.ProfilePictureUrl
                } : null
            };
        }
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(user => new UserDto
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
                Profile = user.Profile != null ? new ProfileDto
                {
                    ProfileId = user.Profile.ProfileId,
                    Bio = user.Profile.Bio,
                    Address = user.Profile.Address,
                    ProfilePictureUrl = user.Profile.ProfilePictureUrl
                } : null
            }).ToList();
        }
    }

    public class GetUsersByStatusQueryHandler : IRequestHandler<GetUsersByStatusQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersByStatusQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUsersByStatusQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetByStatusAsync(request.Status);

            return users.Select(user => new UserDto
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
                Profile = user.Profile != null ? new ProfileDto
                {
                    ProfileId = user.Profile.ProfileId,
                    Bio = user.Profile.Bio,
                    Address = user.Profile.Address,
                    ProfilePictureUrl = user.Profile.ProfilePictureUrl
                } : null
            }).ToList();
        }
    }

    public class GetUsersByTypeQueryHandler : IRequestHandler<GetUsersByTypeQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersByTypeQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUsersByTypeQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetByUserTypeAsync(request.UserType);

            return users.Select(user => new UserDto
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
                Profile = user.Profile != null ? new ProfileDto
                {
                    ProfileId = user.Profile.ProfileId,
                    Bio = user.Profile.Bio,
                    Address = user.Profile.Address,
                    ProfilePictureUrl = user.Profile.ProfilePictureUrl
                } : null
            }).ToList();
        }
    }
}
