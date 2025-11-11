using MediatR;
using Application.Dtos;

namespace Application.Queries.EmployerQuery
{
    public class GetEmployerByUserIdQuery : IRequest<EmployerDto>
    {
        public string UserId { get; set; }

        public GetEmployerByUserIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
