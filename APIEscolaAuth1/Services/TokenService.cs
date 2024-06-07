using APIEscolaAuth1.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIEscolaAuth1.Services;

public class TokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public string Generate(User user)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(getKey());
        var credencials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = credencials
        };
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(User user)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim(ClaimTypes.Name, user.UserName!));
        ci.AddClaim(new Claim(ClaimTypes.Email, user.Email!));

        if (user.Role is not null)
            ci.AddClaim(new Claim(ClaimTypes.Role, user.Role));

        return ci;
    }

    public string getKey()
    {
        return _config.GetSection("Key").GetValue<string>("PrivateKey") ??
            throw new InvalidOperationException("Secret key not found!");
    }
}
