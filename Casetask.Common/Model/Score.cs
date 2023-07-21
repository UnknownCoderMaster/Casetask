namespace Casetask.Common.Model;

public class Score : BaseEntity
{
    public int SubjectId { get; set; }
    public Subject Subject { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; }
    public int? Value { get; set; }
}
