using Casetask.Common.Dtos.UserDTOs;
using Casetask.Common.Interfaces;
using Casetask.Common.Model;
using System.Linq.Expressions;

namespace Casetask.Business.Services;

public class UserService : IUserService
{
	public Task<bool> CreateAsync(UserRegisterDto userRegisterDto)
	{
		throw new NotImplementedException();
	}

	public Task<bool> DeleteAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<List<UserViewDto>> GetAllAsync(Expression<Func<User, bool>> expression = null)
	{
		throw new NotImplementedException();
	}

	public Task<UserViewDto> GetAsync(Expression<Func<User, bool>> expression)
	{
		throw new NotImplementedException();
	}

	public Task<bool> UpdateAsync(int id, UserUpdateDto userUpdateDto)
	{
		throw new NotImplementedException();
	}
}
