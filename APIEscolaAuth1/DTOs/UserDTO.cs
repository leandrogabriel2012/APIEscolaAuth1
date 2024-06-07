using System.ComponentModel.DataAnnotations;

namespace APIEscolaAuth1.DTOs;

public class UserDTO
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }

    public string? Role { get; set; }
}
