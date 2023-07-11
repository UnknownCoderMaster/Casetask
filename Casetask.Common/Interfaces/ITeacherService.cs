using Casetask.Common.Dtos.Teacher;

namespace Casetask.Common.Interfaces;

public interface ITeacherService
{
    Task<int> CreateTeacherAsync(TeacherCreate teacherCreate);
    Task UpdateTeacherAsync(TeacherUpdate teacherUpdate);
    Task DeleteTeacherAsync(TeacherDelete teacherDelete);
    Task<TeacherGet> GetTeacherAsync(int id);
    Task<List<TeacherGet>> GetTeachersAsync();
}
