namespace Casetask.Common.Model;

public class Subject : BaseEntity
{
    public string? Name { get; set; }
    public int? TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
}