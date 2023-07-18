using AutoMapper;
using Casetask.Common.Dtos.StudentDTOs;
using Casetask.Common.Dtos.SubjectDtos;
using Casetask.Common.Dtos.Teacher;
using Casetask.Common.Model;

namespace Casetask.Business;

public class DtoEntityMapperProfile : Profile
{
    public DtoEntityMapperProfile()
    {
        CreateMap<TeacherCreate, Teacher>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Subjects, opt => opt.Ignore());
        CreateMap<TeacherUpdate, Teacher>();
        CreateMap<Teacher, TeacherGet>();

        CreateMap<SubjectCreate, Subject>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<SubjectUpdate, Subject>();
        CreateMap<Subject, SubjectGet>();

        CreateMap<StudentCreate, Student>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<StudentUpdate, Student>();
        CreateMap<Student, StudentGet>();
    }
}