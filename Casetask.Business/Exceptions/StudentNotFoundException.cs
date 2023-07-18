using System.Runtime.Serialization;

namespace Casetask.Business.Exceptions;

public class StudentNotFoundException : Exception
{
    public int Id;

    public StudentNotFoundException()
    {
    }

    public StudentNotFoundException(int id)
    {
        Id = id;
    }

    public StudentNotFoundException(string? message) : base(message)
    {
    }

    public StudentNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected StudentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
