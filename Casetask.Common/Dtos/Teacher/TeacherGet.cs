using Casetask.Common.Dtos.SubjectDtos;

namespace Casetask.Common.Dtos.Teacher;

public record TeacherGet(string FirstName, string LastName, string PhoneNumber, string Email, DateTime BirthDate, List<SubjectGet> Subjects);
