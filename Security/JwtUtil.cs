using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth_Crud.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Auth_Crud.Security;

public class JwtUtil : IJwtUtil
{
    private readonly IConfiguration _configuration;

    public JwtUtil(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Admin admin)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _configuration["JwtSettings:Audience"],
            Issuer = _configuration["JwtSettings:Issuer"],
            Expires = DateTime.Now.AddMinutes(int.Parse(_configuration["JwtSettings:ExpiresInMinutes"])),
            IssuedAt = DateTime.Now,
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.Email, admin.Email),
                new(ClaimTypes.NameIdentifier, admin.Id.ToString())
            }),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}