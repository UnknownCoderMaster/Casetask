using Casetask.Common.Dtos.SubjectDtos;

namespace Casetask.Common.Interfaces;

public interface ISubjectService
{
    Task<int> CreateSubjectAsync(SubjectCreate subjectCreate);
    Task UpdateSubjectAsync(SubjectUpdate subjectUpdate);
    Task DeleteSubjectAsync(SubjectDelete subjectDelete);
    Task<SubjectGet> GetSubjectAsync(int id);
    Task<List<SubjectGet>> GetSubjectsAsync();
}