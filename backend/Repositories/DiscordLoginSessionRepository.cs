using backend.Entities;
using backend.Responses;
using backend.Setup;

namespace backend.Repositories;

public class DiscordLoginSessionRepository(DashboardDbContext context) : IDiscordLoginSessionRepository
{
    public async Task<DiscordLoginSession> CreateAsync(User user, AccessTokenResponse accessTokenResponse)
    {
        var session = new DiscordLoginSession
        {
            AccessToken = accessTokenResponse.AccessToken,
            ExpiresIn = accessTokenResponse.ExpiresIn,
            RefreshToken = accessTokenResponse.RefreshToken,
            TokenType = accessTokenResponse.TokenType,
            Scope = accessTokenResponse.Scope,
            UserId = user.Id
        };
        context.DiscordLoginSessions.Add(session);
        await context.SaveChangesAsync();
        return session;
    }

    public async Task PersistAsync(DiscordLoginSession session)
    {
        context.DiscordLoginSessions.Update(session);
        await context.SaveChangesAsync();
    }
}