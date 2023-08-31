using System.Runtime.Serialization;

namespace Casetask.Business.Exceptions;

public class UserNotFoundException : Exception
{
	public int Id;

	public UserNotFoundException()
	{
	}

	public UserNotFoundException(int id, string message) : base(message)
	{
		Id = id;
	}

	public UserNotFoundException(string? message) : base(message)
	{
	}

	public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
	{
	}

	protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
