namespace Casetask.Common.Dtos.Teacher;

public record TeacherCreate(string FirstName, string LastName, string PhoneNumber, string Email, DateTime BirthDate, List<string> Subjects);