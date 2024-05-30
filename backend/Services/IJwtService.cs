using System.IdentityModel.Tokens.Jwt;
using backend.Entities;

namespace backend.Services;

public interface IJwtService
{
    Task<JwtSecurityToken> GenerateTokenForUserAsync(User user);
    Task<string> ToString(JwtSecurityToken token);
}