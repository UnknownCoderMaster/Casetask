namespace Casetask.Common.Model;

public class Teacher : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public DateTime BirthDate { get; set; }
    public List<Subject> Subjects { get; set; } = new List<Subject> { };
}
