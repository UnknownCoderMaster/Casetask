using Casetask.Common.Dtos.UserDTOs;
using Casetask.Common.Model;
using System.Linq.Expressions;

namespace Casetask.Common.Interfaces;

public interface IUserService
{
	Task<bool> CreateAsync(UserRegisterDto userRegisterDto);
	Task<bool> UpdateAsync(int id, UserUpdateDto userUpdateDto);
	Task<bool> DeleteAsync(int id);
	Task<UserViewDto> GetAsync(Expression<Func<User, bool>> expression);
	Task<List<UserViewDto>> GetAllAsync(Expression<Func<User, bool>> expression = null);
}
