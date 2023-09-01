using AutoMapper;
using Casetask.Business.Exceptions;
using Casetask.Business.Extensions;
using Casetask.Common.Dtos.UserDTOs;
using Casetask.Common.Interfaces;
using Casetask.Common.Model;
using System.Linq.Expressions;

namespace Casetask.Business.Services;

public class UserService : IUserService
{
	private readonly IGenericRepository<User> UserRepository;
	private IMapper _mapper { get; }

    public UserService(IGenericRepository<User> userRepository, IMapper mapper)
    {
		UserRepository = userRepository;
		_mapper = mapper;
	}


	public async Task<bool> CreateAsync(UserRegisterDto userRegisterDto)
	{
		if (userRegisterDto.Password != userRegisterDto.ConfirmPassword)
			throw new UserException("Passwords are different");

		var existEmails = await UserRepository.GetFilteredAsync(new Expression<Func<User, bool>>[] { u => u.Email == userRegisterDto.Email },null, null);

		if (existEmails.Count > 0)
			throw new UserException("This email is already taken");

		var createdUser = _mapper.Map<User>(userRegisterDto);

		int userId = await UserRepository.InsertAsync(createdUser);

		createdUser.Password = createdUser.Password.Encrypt();

		await UserRepository.SaveChangesAsync();

		return true;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var existingUser = await UserRepository.GetByIdAsync(id);

		if (existingUser is null)
			throw new UserException(404, $"This {id} user is not find");

		UserRepository.Delete(existingUser);

		return true;
	}

	public async Task<List<UserViewDto>> GetAllAsync(Expression<Func<User, bool>> expression = null)
	{
		var users = await UserRepository.GetAsync(null, null);

		return _mapper.Map<List<UserViewDto>>(users);
	}

	public async Task<UserViewDto> GetAsync(int id)
	{
		var user = await UserRepository.GetByIdAsync(id);

		if (user is null)
			throw new UserException(404, "User not found");

		return _mapper.Map<UserViewDto>(user);
	}

	public async Task<bool> UpdateAsync(int id, UserUpdateDto userUpdateDto)
	{
		var existUser = await UserRepository.GetByIdAsync(id);

		if (existUser is null)
			throw new UserException(404, "This user not found");

		var alreadyExistUsers = await UserRepository.GetAsync(
			null, null, u => u.Email == userUpdateDto.Email && u.Id != id);

		if (alreadyExistUsers.Count > 0)
			throw new UserException("User with such email already exists");

		UserRepository.Update(_mapper.Map(userUpdateDto, existUser));

		await UserRepository.SaveChangesAsync();

		return true;
	}
}
