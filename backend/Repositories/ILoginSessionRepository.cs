using System.IdentityModel.Tokens.Jwt;
using backend.Entities;

namespace backend.Repositories;

public interface ILoginSessionRepository
{
    Task<LoginSession> Create(User user, string jwtToken);
}