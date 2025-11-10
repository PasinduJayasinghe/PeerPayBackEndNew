using Application.Dtos;
using MediatR;

namespace Application.Queries.StudentQuery
{
    public class GetStudentByIdQuery : IRequest<StudentProfileDto>
    {
        public string StudentId { get; set; }
    }
}
