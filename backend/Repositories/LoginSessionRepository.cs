using System.IdentityModel.Tokens.Jwt;
using backend.Entities;
using backend.Setup;

namespace backend.Repositories;

public class LoginSessionRepository(DashboardDbContext context) : ILoginSessionRepository
{
    public async Task<LoginSession> Create(User user, string jwtToken)
    {
        var session = new LoginSession
        {
            UserId = user.Id
        };
        session.Refresh();
        session.AccessToken = jwtToken;
        context.LoginSessions.Add(session);
        await context.SaveChangesAsync();
        return session;
    }
}