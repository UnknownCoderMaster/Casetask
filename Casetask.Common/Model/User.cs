using Casetask.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Casetask.Common.Model;

public class User : BaseEntity
{

    [MaxLength(100)]
    public string Email { get; set; }

    [MinLength(8)]
    public string Password { get; set; }
    public UserRole Role { get; set; }
}
