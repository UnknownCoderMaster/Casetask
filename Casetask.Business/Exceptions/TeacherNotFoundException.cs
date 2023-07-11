using System.Runtime.Serialization;

namespace Casetask.Business.Exceptions;

public class TeacherNotFoundException : Exception
{
    public int Id;

    public TeacherNotFoundException()
    {
    }

    public TeacherNotFoundException(int id)
    {
        Id = id;
    }

    public TeacherNotFoundException(string? message) : base(message)
    {
    }

    public TeacherNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected TeacherNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
