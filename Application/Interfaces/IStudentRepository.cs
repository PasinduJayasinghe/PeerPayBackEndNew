using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> GetStudentByIdAsync(string studentId);
        Task<Student> GetByUserIdAsync(string userId);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<IEnumerable<Student>> GetByUniversityAsync(string university);
        Task<IEnumerable<Student>> SearchBySkillsAsync(string[] skills);
        Task<Student> AddAsync(Student student);
        Task UpdateStudentAsync(Student student);
    }
}
