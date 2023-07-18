using Casetask.Common.Dtos.SubjectDtos;

namespace Casetask.Common.Dtos.StudentDTOs;

public record StudentGet(int Id, string FirstName, string LastName, string PhoneNumber, string Email, DateTime BirthDate, int StudentRegNumber, List<SubjectGetForStudent> Scores);