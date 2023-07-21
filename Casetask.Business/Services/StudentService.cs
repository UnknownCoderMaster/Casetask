using AutoMapper;
using Casetask.Business.Exceptions;
using Casetask.Common.Dtos.StudentDTOs;
using Casetask.Common.Dtos.SubjectDtos;
using Casetask.Common.Interfaces;
using Casetask.Common.Model;
using System.Linq.Expressions;

namespace Casetask.Business.Services;

public class StudentService : IStudentService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Student> StudentRepository { get; }
    private IGenericRepository<Subject> SubjectRepository { get; }
    private IGenericRepository<Score> ScoreRepository { get; }

    public StudentService(IMapper mapper, IGenericRepository<Student> studentRepository,
        IGenericRepository<Subject> subjectRepository, IGenericRepository<Score> scoreRepository)
    {
        Mapper = mapper;
        StudentRepository = studentRepository;
        SubjectRepository = subjectRepository;
        ScoreRepository = scoreRepository;
    }

    public async Task<int> CreateStudentAsync(StudentCreate studentCreate)
    {
        var entity = Mapper.Map<Student>(studentCreate);

        await StudentRepository.InsertAsync(entity);

        await StudentRepository.SaveChangesAsync();

        foreach(int subjectId in studentCreate.Subjects)
            await ScoreRepository.InsertAsync(new Score { SubjectId = subjectId, StudentId = entity.Id });
        
        await ScoreRepository.SaveChangesAsync();

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
        
        Expression<Func<Score, bool>> filterScore;

        List<Expression<Func<Subject, bool>>> filterSubjects = new List<Expression<Func<Subject, bool>>>();

        var existingScores = await ScoreRepository.GetFilteredAsync(new[] { filterScore = s => s.StudentId == id }, null, null );

        foreach (var score in existingScores)
            filterSubjects.Add(sub => sub.Id == score.SubjectId);

        var existingSubjects = await SubjectRepository.GetFilteredAsync(filterSubjects.ToArray(), null, null);


        var studentForView = Mapper.Map<StudentGet>(entity);

        studentForView.Scores = existingScores.Join(
            existingSubjects,
            score => score.SubjectId,
            subject => subject.Id,
            (score, subject) => new SubjectGetForStudent
            {
                SubjectName = subject.Name,
                Score = (int)score.Value
            });

        return studentForView;
    }

    public async Task<List<StudentGet>> GetStudentsAsync()
    {
        var students = await StudentRepository.GetAsync(null, null);

        Expression<Func<Subject, bool>> filter;


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
