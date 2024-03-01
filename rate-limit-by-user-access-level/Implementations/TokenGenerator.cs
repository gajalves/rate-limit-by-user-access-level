using Microsoft.IdentityModel.Tokens;
using rate_limit_by_user_access_level.Abstractions;
using rate_limit_by_user_access_level.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace rate_limit_by_user_access_level.Implementations;

public class TokenGenerator : ITokenGenerator
{
    private readonly IConfiguration _configuration;
    private readonly RateLimitService _rateLimitService;

    public TokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
        _rateLimitService = new RateLimitService();
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(ClaimTypes.Email, user.Email),
                 new Claim(ClaimTypes.Role, user.AccessLevel.ToString())                 
            }),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpirationInMinutes"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
