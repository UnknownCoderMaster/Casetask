using Casetask.Common.Enums;

namespace Casetask.Common.Dtos.UserDTOs;

public class UserViewDto
{
	public int Id { get; set; }
	public string Email { get; set; }
    public UserRole Role { get; set; }
}
