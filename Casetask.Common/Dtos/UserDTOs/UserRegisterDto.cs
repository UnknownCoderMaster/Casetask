using System.ComponentModel.DataAnnotations;

namespace Casetask.Common.Dtos.UserDTOs;

public class UserRegisterDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
