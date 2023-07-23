using AutoMapper;
using Casetask.Business.Exceptions;
using Casetask.Common.Dtos.Teacher;
using Casetask.Common.Interfaces;
using Casetask.Common.Model;
using System.Linq.Expressions;

namespace Casetask.Business.Services;

public class TeacherService : ITeacherService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Teacher> TeacherRepository { get; }
    private IGenericRepository<Subject> SubjectRepository { get; }

    public TeacherService(IMapper mapper, IGenericRepository<Teacher> teacherRepository, IGenericRepository<Subject> subjectRepository)
    {
        Mapper = mapper;
        TeacherRepository = teacherRepository;
        SubjectRepository = subjectRepository;
    }


    public async Task<int> CreateTeacherAsync(TeacherCreate teacherCreate)
    {
        var entity = Mapper.Map<Teacher>(teacherCreate);

        List<Expression<Func<Subject, bool>>> filters = new List<Expression<Func<Subject, bool>>>();

        foreach (var subject in teacherCreate.Subjects)
            filters.Add(s => s.Name == subject);

        var existingSubjects = await SubjectRepository.GetFilteredAsync(filters.ToArray(), null, null);

        entity.Subjects = existingSubjects;

        await TeacherRepository.InsertAsync(entity);

        await TeacherRepository.SaveChangesAsync();

        foreach (var subject in existingSubjects)
        {
            subject.TeacherId = entity.Id;
            SubjectRepository.Update(subject);
        }

        await SubjectRepository.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteTeacherAsync(TeacherDelete teacherDelete)
    {
        var entity = await TeacherRepository.GetByIdAsync(teacherDelete.Id);

        if (entity == null)
            throw new TeacherNotFoundException(teacherDelete.Id);

        TeacherRepository.Delete(entity);
        await TeacherRepository.SaveChangesAsync();
    }

    public async Task<TeacherGet> GetTeacherAsync(int id)
    {
        var entity = await TeacherRepository.GetByIdAsync(id);

        if (entity == null)
            throw new TeacherNotFoundException(id);

        Expression<Func<Subject, bool>> filter = s => s.TeacherId == id;

        var existingSubjects = await SubjectRepository.GetFilteredAsync(new[] { filter }, null, null);

        entity.Subjects = existingSubjects;

        return Mapper.Map<TeacherGet>(entity);
    }

    public async Task<List<TeacherGet>> GetTeachersAsync()
    {
        var teachers = await TeacherRepository.GetAsync(null, null);

        Expression<Func<Subject, bool>> filter;

        foreach (var teacher in teachers)
            teacher.Subjects = await SubjectRepository.GetFilteredAsync(new[] { filter = s => s.TeacherId == teacher.Id }, null, null);

        return Mapper.Map<List<TeacherGet>>(teachers);
    }

    public async Task UpdateTeacherAsync(TeacherUpdate teacherUpdate)
    {
        var existingTeacher = await TeacherRepository.GetByIdAsync(teacherUpdate.Id);

        if (existingTeacher == null)
            throw new TeacherNotFoundException(teacherUpdate.Id);

        var entity = Mapper.Map<Teacher>(teacherUpdate);
        TeacherRepository.Update(entity);
        await TeacherRepository.SaveChangesAsync();
    }
}