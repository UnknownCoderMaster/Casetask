using System.Runtime.Serialization;

namespace Casetask.Business.Exceptions;

public class SubjectNotFoundException : Exception
{
    public int Id;

    public SubjectNotFoundException()
    {
    }

    public SubjectNotFoundException(int id)
    {
        Id = id;
    }

    public SubjectNotFoundException(string? message) : base(message)
    {
    }

    public SubjectNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected SubjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
