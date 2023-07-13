using Casetask.Common.Dtos.SubjectDtos;

namespace Casetask.Common.Dtos.Teacher;

public record TeacherUpdate(int Id, string FirstName, string LastName, string PhoneNumber, string Email, DateTime BirthDate, List<SubjectUpdate> Subjects);/**/