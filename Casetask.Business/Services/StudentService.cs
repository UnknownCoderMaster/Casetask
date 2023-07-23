using AutoMapper;
using Casetask.Business.Exceptions;
using Casetask.Common.Dtos.StudentDTOs;
using Casetask.Common.Dtos.SubjectDtos;
using Casetask.Common.Interfaces;
using Casetask.Common.Model;
using System.Linq.Expressions;
using static System.Formats.Asn1.AsnWriter;

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
        
        Expression<Func<Score, bool>> filterScore = s => s.StudentId == id;

        Expression<Func<Subject, bool>> f;

        var existingScores = await ScoreRepository.GetFilteredAsync(new[] { filterScore }, null, null );

        List<Subject> existingSubjects = new List<Subject>();

        foreach (var score in existingScores)
            existingSubjects.AddRange(await SubjectRepository.GetFilteredAsync(new[] { f = sub => sub.Id == score.SubjectId } , null, null));

        var studentForView = Mapper.Map<StudentGet>(entity);

        studentForView.Scores = existingScores.Join(
            existingSubjects,
            score => score.SubjectId,
            subject => subject.Id,
            (score, subject) => new SubjectGetForStudent
            {
                SubjectName = subject.Name,
                Score = score.Value
            }).ToList();

        return studentForView;
    }

    public async Task<List<StudentGet>> GetStudentsAsync()
    {
        var students = await StudentRepository.GetAsync(null, null);

        var subjects = await SubjectRepository.GetAsync(null, null);

        var scores = await ScoreRepository.GetAsync(null, null);

        var studentGetView = scores.
            Join(students, score => score.StudentId, student => student.Id, (score, student) => new
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email,
                BirthDate = student.BirthDate,
                StudentRegNumber = student.StudentRegNumber,
                subjectId = score.SubjectId, 
                Value = score.Value
            })
            .Join(subjects, stu => stu.subjectId , subject => subject.Id, (stu, subject) => new
            {
                Id = stu.Id,
                FirstName = stu.FirstName,
                LastName = stu.LastName,
                PhoneNumber = stu.PhoneNumber,
                Email = stu.Email,
                BirthDate = stu.BirthDate,
                StudentRegNumber = stu.StudentRegNumber,
                SubjectName = subject.Name,
                Value = stu.Value
            })
            .GroupBy(stu => new
            {
                stu.Id,
                stu.FirstName,
                stu.LastName,
                stu.PhoneNumber,
                stu.Email,
                stu.BirthDate,
                stu.StudentRegNumber
            })
            .Select(groupedStu => new StudentGet
            {
                Id = groupedStu.Key.Id,
                FirstName = groupedStu.Key.FirstName,
                LastName = groupedStu.Key.LastName,
                PhoneNumber = groupedStu.Key.PhoneNumber,
                Email = groupedStu.Key.Email,
                BirthDate = groupedStu.Key.BirthDate,
                StudentRegNumber = groupedStu.Key.StudentRegNumber,
                Scores = groupedStu.Select(stu => new SubjectGetForStudent
                {
                    SubjectName = stu.SubjectName,
                    Score = stu.Value
                }).ToList()
            })
            .ToList();

        return studentGetView;
        //return Mapper.Map<List<StudentGet>>(students);
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
