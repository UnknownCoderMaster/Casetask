using Casetask.Common.Dtos.UserDTOs;
using Casetask.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Casetask.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService AuthService;
		private readonly IUserService UserService;

		public AuthController(IAuthService authService, IUserService userService)
        {
			AuthService = authService;
			UserService = userService;
		}

        
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
		{
			var token = await AuthService.GenerateToken(userLoginDto.Email, userLoginDto.Password);

			return Ok(token);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
			=> Ok(await UserService.CreateAsync(userRegisterDto));
	}
}
