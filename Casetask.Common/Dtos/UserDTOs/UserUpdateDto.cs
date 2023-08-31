using System.ComponentModel.DataAnnotations;

namespace Casetask.Common.Dtos.UserDTOs;

public class UserUpdateDto
{
    [Required]
    public string Email { get; set; }
}
