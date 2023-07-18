using AutoMapper;
using Casetask.Business.Exceptions;
using Casetask.Common.Dtos.StudentDTOs;
using Casetask.Common.Dtos.Teacher;
using Casetask.Common.Interfaces;
using Casetask.Common.Model;
using System.Linq.Expressions;

namespace Casetask.Business.Services;

public class StudentService : IStudentService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Student> StudentRepository { get; }
    private IGenericRepository<Subject> SubjectRepository { get; }

    public StudentService(IMapper mapper, IGenericRepository<Student> studentRepository, IGenericRepository<Subject> subjectRepository)
    {
        Mapper = mapper;
        StudentRepository = studentRepository;
        SubjectRepository = subjectRepository;
    }

    public async Task<int> CreateStudentAsync(StudentCreate studentCreate)
    {
        var entity = Mapper.Map<Student>(studentCreate);

        List<Expression<Func<Subject, bool>>> filters = new List<Expression<Func<Subject, bool>>>();

        foreach (var subject in studentCreate.Subjects)
            filters.Add(s => s.Name == subject);

        var existingSubjects = await SubjectRepository.GetFilteredAsync(filters.ToArray(), null, null);

        entity.Subjects = existingSubjects;

        await StudentRepository.InsertAsync(entity);

        await StudentRepository.SaveChangesAsync();

        foreach (var subject in existingSubjects)
        {
            subject.StudentId = entity.Id;
            SubjectRepository.Update(subject);
        }

        await SubjectRepository.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteStudentAsync(StudentDelete studentDelete)
    {
        var entity = await StudentRepository.GetByIdAsync(studentDelete.Id);

        if(entity == null)
            throw new StudentNotFoundException(studentDelete.Id);

        StudentRepository.Delete(entity);
        await SubjectRepository.SaveChangesAsync();
    }

    public async Task<StudentGet> GetStudentAsync(int id)
    {
        var entity = await StudentRepository.GetByIdAsync(id);

        if (entity == null)
            throw new StudentNotFoundException(id);

        Expression<Func<Subject, bool>> filter = s => s.StudentId == id;

        var existingSubjects = await SubjectRepository.GetFilteredAsync(new[] { filter }, null, null);

        entity.Subjects = existingSubjects;

        return Mapper.Map<StudentGet>(entity);
    }

    public async Task<List<StudentGet>> GetStudentsAsync()
    {
        var students = await StudentRepository.GetAsync(null, null);

        Expression<Func<Subject, bool>> filter;

        foreach (var student in students)
            student.Subjects = await SubjectRepository.GetFilteredAsync(new[] { filter = s => s.StudentId == student.Id }, null, null);

        return Mapper.Map<List<StudentGet>>(students);
    }

    public async Task UpdateStudentAsync(StudentUpdate studentUpdate)
    {
        var existingStudent = await StudentRepository.GetByIdAsync(studentUpdate.Id);

        if (existingStudent == null)
            throw new TeacherNotFoundException(studentUpdate.Id);

        var entity = Mapper.Map<Student>(studentUpdate);
        StudentRepository.Update(entity);
        await StudentRepository.SaveChangesAsync();
    }
}
