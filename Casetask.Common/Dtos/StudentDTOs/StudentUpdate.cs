namespace Casetask.Common.Dtos.StudentDTOs;

public record StudentUpdate(int Id, string FirstName, string LastName, string PhoneNumber, string Email, DateTime BirthDate, int StudentRegNumber, List<SubjectDtos> Subjects);
