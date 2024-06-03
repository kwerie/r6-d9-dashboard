using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Entities;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services;

public class JwtService(
    string jwtKey,
    string issuer,
    string audience,
    string algo = SecurityAlgorithms.HmacSha256
) : IJwtService
{
    public Task<JwtSecurityToken> GenerateTokenForUserAsync(User user, string refreshToken)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, algo);
        var claims = new[]
        {
            new Claim("username", user.Username),
            new Claim("email", user.Email),
            new Claim("user_id", user.Id.ToString())
        };

        return Task.FromResult(
            new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials
            )
        );
    }

    public async Task<string> ToString(JwtSecurityToken token)
    {
        return await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token));
    }
}