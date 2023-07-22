using Casetask.Common.Dtos.SubjectDtos;

namespace Casetask.Common.Dtos.StudentDTOs;

public class StudentGet {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public int StudentRegNumber { get; set; }
    public List<SubjectGetForStudent> Scores { get; set; } = default!;
}