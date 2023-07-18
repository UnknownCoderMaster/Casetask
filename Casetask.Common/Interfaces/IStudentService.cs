using Casetask.Common.Dtos.StudentDTOs;

namespace Casetask.Common.Interfaces;

public interface IStudentService
{
    Task<int> CreateStudentAsync(StudentCreate studentCreate);
    Task UpdateStudentAsync(StudentUpdate studentUpdate);
    Task DeleteStudentAsync(StudentDelete studentDelete);
    Task<StudentGet> GetStudentAsync(int id);
    Task<List<StudentGet>> GetStudentsAsync();
}
