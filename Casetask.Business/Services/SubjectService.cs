using AutoMapper;
using Casetask.Business.Exceptions;
using Casetask.Common.Dtos.SubjectDtos;
using Casetask.Common.Interfaces;
using Casetask.Common.Model;

namespace Casetask.Business.Services;

public class SubjectService : ISubjectService
{

    private IMapper Mapper { get; }
    private IGenericRepository<Subject> SubjectRepository { get; }

    public SubjectService(IMapper mapper, IGenericRepository<Subject> subjectRepository)
    {
        Mapper = mapper;
        SubjectRepository = subjectRepository;
    }

    public async Task<int> CreateSubjectAsync(SubjectCreate subjectCreate)
    {
        var entity = Mapper.Map<Subject>(subjectCreate);

        await SubjectRepository.InsertAsync(entity);
        await SubjectRepository.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteSubjectAsync(SubjectDelete subjectDelete)
    {
        var entity = await SubjectRepository.GetByIdAsync(subjectDelete.Id);

        if (entity == null)
            throw new SubjectNotFoundException(entity.Id);

        SubjectRepository.Delete(entity);
        await SubjectRepository.SaveChangesAsync();
    }

    public async Task<SubjectGet> GetSubjectAsync(int id)
    {
        var entity = await SubjectRepository.GetByIdAsync(id);

        if(entity == null)
            throw new SubjectNotFoundException(id);

        return Mapper.Map<SubjectGet>(entity);
    }

    public async Task<List<SubjectGet>> GetSubjectsAsync()
    {
        var entities = await SubjectRepository.GetAsync(null, null);
        return Mapper.Map<List<SubjectGet>>(entities);
    }

    public async Task UpdateSubjectAsync(SubjectUpdate subjectUpdate)
    {
        var existingSubject= await SubjectRepository.GetByIdAsync(subjectUpdate.Id);

        if (existingSubject == null)
            throw new SubjectNotFoundException(subjectUpdate.Id);

        var entity = Mapper.Map<Subject>(existingSubject);
        SubjectRepository.Update(entity);
        await SubjectRepository.SaveChangesAsync();
    }
}
