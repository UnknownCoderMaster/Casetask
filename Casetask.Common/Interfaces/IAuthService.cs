namespace Casetask.Common.Interfaces;

public interface IAuthService
{
	Task<string> GenerateToken(string email, string password);
}
