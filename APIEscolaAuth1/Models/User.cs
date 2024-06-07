using Microsoft.AspNetCore.Identity;

namespace APIEscolaAuth1.Models;

public class User : IdentityUser
{
    public string? Role { get; set; }
}

