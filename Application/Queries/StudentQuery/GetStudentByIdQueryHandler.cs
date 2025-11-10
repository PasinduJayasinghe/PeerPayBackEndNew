using Application.Dtos;
using Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.StudentQuery
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentProfileDto>
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentByIdQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentProfileDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentByIdAsync(request.StudentId);
            
            if (student == null)
            {
                throw new Exception($"Student with ID {request.StudentId} not found");
            }

            return new StudentProfileDto
            {
                StudentId = student.StudentId,
                UserId = student.UserId,
                Name = student.User?.Name,
                Email = student.User?.Email,
                Phone = student.User?.Phone,
                University = student.University,
                Course = student.Course,
                YearOfStudy = student.YearOfStudy,
                AcademicVerificationStatus = student.AcademicVerificationStatus,
                Rating = student.Rating,
                CompletedJobs = student.CompletedJobs,
                TotalEarnings = student.TotalEarnings,
                CvUrl = student.CvUrl,
                Bio = student.User?.Profile?.Bio,
                Address = student.User?.Profile?.Address,
                ProfilePictureUrl = student.User?.Profile?.ProfilePictureUrl
            };
        }
    }
}
