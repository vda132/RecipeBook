using System.ComponentModel.DataAnnotations;

namespace DTO;

public class UserRegisterDTO
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Login { get; set; }

    [Required]
    public string Password { get; set; }
}
