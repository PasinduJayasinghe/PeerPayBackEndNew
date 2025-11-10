using Application.Dtos;
using Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.StudentCommand
{
    public class UpdateStudentProfileCommandHandler : IRequestHandler<UpdateStudentProfileCommand, StudentProfileDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;

        public UpdateStudentProfileCommandHandler(
            IStudentRepository studentRepository,
            IUserRepository userRepository)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
        }

        public async Task<StudentProfileDto> Handle(UpdateStudentProfileCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentByIdAsync(request.StudentId);
            
            if (student == null)
            {
                throw new Exception($"Student with ID {request.StudentId} not found");
            }

            // Update student fields
            if (!string.IsNullOrEmpty(request.University))
                student.University = request.University;

            if (!string.IsNullOrEmpty(request.Course))
                student.Course = request.Course;

            if (request.YearOfStudy.HasValue)
                student.YearOfStudy = request.YearOfStudy.Value;

            if (!string.IsNullOrEmpty(request.CvUrl))
                student.CvUrl = request.CvUrl;

            // Update user fields
            if (student.User != null)
            {
                if (!string.IsNullOrEmpty(request.Name))
                    student.User.Name = request.Name;

                if (!string.IsNullOrEmpty(request.Phone))
                    student.User.Phone = request.Phone;

                // Update profile
                if (student.User.Profile != null)
                {
                    if (!string.IsNullOrEmpty(request.Bio))
                        student.User.Profile.Bio = request.Bio;

                    if (!string.IsNullOrEmpty(request.Address))
                        student.User.Profile.Address = request.Address;
                }
            }

            await _studentRepository.UpdateStudentAsync(student);

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
