namespace Casetask.Common.Dtos.StudentDTOs;

public record StudentCreate(string FirstName, string LastName, string PhoneNumber, string Email, DateTime BirthDate, int StudentRegNumber, List<string> Subjects);