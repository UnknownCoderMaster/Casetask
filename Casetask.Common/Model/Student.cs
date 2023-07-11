namespace Casetask.Common.Model;

public class Student : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public DateTime BirthDate { get; set; }
    public int StudentRegNumber { get; set; }
    public List<Subject> Subjects { get; set; }
    public List<Score>? Results { get; set; }
}
