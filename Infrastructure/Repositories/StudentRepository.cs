using Application.Interfaces;
using Domain.Classes;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly PeerPayDbContext _context;

        public StudentRepository(PeerPayDbContext context)
        {
            _context = context;
        }

        public async Task<Student> GetByIdAsync(string studentId)
        {
            return await _context.Students
                .Include(s => s.User)
                .ThenInclude(u => u.Profile)
                .Include(s => s.Skills)
                .Include(s => s.Earnings)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);
        }

        public async Task<Student> GetStudentByIdAsync(string studentId)
        {
            return await GetByIdAsync(studentId);
        }

        public async Task<Student> GetByUserIdAsync(string userId)
        {
            return await _context.Students
                .Include(s => s.User)
                .ThenInclude(u => u.Profile)
                .Include(s => s.Skills)
                .Include(s => s.Earnings)
                .Include(s => s.Applications)
                .FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public async Task<IEnumerable<Student>> GetByUniversityAsync(string university)
        {
            return await _context.Students
                .Include(s => s.User)
                .Include(s => s.Skills)
                .Where(s => s.University.ToLower().Contains(university.ToLower()))
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> SearchBySkillsAsync(string[] skills)
        {
            return await _context.Students
                .Include(s => s.User)
                .Include(s => s.Skills)
                .Where(s => s.Skills.Any(sk => skills.Contains(sk.SkillName)))
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
                .Include(s => s.User)
                .Include(s => s.Skills)
                .ToListAsync();
        }

        public async Task<Student> AddAsync(Student student)
        {
            student.StudentId = Guid.NewGuid().ToString();
            student.Rating = 0;
            student.CompletedJobs = 0;
            student.TotalEarnings = 0;
            student.AcademicVerificationStatus = "Pending";

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return student;
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            await UpdateAsync(student);
        }

        public async Task DeleteAsync(string studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string studentId)
        {
            return await _context.Students.AnyAsync(s => s.StudentId == studentId);
        }

        public async Task<IEnumerable<Student>> GetTopRatedStudentsAsync(int count)
        {
            return await _context.Students
                .Include(s => s.User)
                .Include(s => s.Skills)
                .OrderByDescending(s => s.Rating)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetByCourseAsync(string course)
        {
            return await _context.Students
                .Include(s => s.User)
                .Include(s => s.Skills)
                .Where(s => s.Course.ToLower().Contains(course.ToLower()))
                .ToListAsync();
        }

        public async Task UpdateRatingAsync(string studentId, decimal newRating)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student != null)
            {
                student.Rating = newRating;
                await _context.SaveChangesAsync();
            }
        }

        public async Task IncrementCompletedJobsAsync(string studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student != null)
            {
                student.CompletedJobs++;
                await _context.SaveChangesAsync();
            }
        }
    }
}
