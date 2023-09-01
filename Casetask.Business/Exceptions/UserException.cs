using System.Runtime.Serialization;

namespace Casetask.Business.Exceptions;

public class UserException : Exception
{
	public int Id;

	public UserException()
	{
	}

	public UserException(int id, string message) : base(message)
	{
		Id = id;
	}

	public UserException(string? message) : base(message)
	{
	}

	public UserException(string? message, Exception? innerException) : base(message, innerException)
	{
	}

	protected UserException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
