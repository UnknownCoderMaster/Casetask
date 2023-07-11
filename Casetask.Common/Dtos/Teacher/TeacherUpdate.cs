using Casetask.Common.Model;

namespace Casetask.Common.Dtos.Teacher;

public record TeacherUpdate(int Id, string FirstName, string LastName, string PhoneNumber, string Email, DateTime BirthDate, List<Subject> Subjects);/**/