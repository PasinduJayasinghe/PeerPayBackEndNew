using Application.Dtos;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.UserQuery
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public string UserId { get; set; }
    }

    public class GetUserByEmailQuery : IRequest<UserDto>
    {
        public string Email { get; set; }
    }

    public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
    {
    }

    public class GetUsersByStatusQuery : IRequest<IEnumerable<UserDto>>
    {
        public UserStatus Status { get; set; }
    }

    public class GetUsersByTypeQuery : IRequest<IEnumerable<UserDto>>
    {
        public UserType UserType { get; set; }
    }
}
