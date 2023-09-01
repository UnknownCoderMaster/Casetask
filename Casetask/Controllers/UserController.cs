using Casetask.Common.Dtos.UserDTOs;
using Casetask.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Casetask.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync([FromRoute] int id, UserUpdateDto userUpdateDto)
			=> Ok(await _userService.UpdateAsync(id, userUpdateDto));

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
			=> Ok(await _userService.GetAllAsync());

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAsync([FromRoute] int id)
			=> Ok(await  (_userService.GetAsync(id)));

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(int id)
			=> Ok(await _userService.DeleteAsync(id));
	}
}
