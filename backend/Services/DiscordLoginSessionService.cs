using backend.Client;
using backend.Entities;
using backend.Repositories;
using backend.Responses;

namespace backend.Services;

public class DiscordLoginSessionService(
    IDiscordLoginSessionRepository discordLoginSessionRepository,
    IDiscordClient discordClient
) : IDiscordLoginSessionService
{
    public async Task<DiscordLoginSession> CreateAsync(User user, AccessTokenResponse accessTokenResponse)
    {
        return await discordLoginSessionRepository.CreateAsync(user, accessTokenResponse);
    }

    public async Task InvalidateAsync(DiscordLoginSession session)
    {
        session.InvalidatedAt = DateTime.UtcNow;
        await discordLoginSessionRepository.PersistAsync(session);
    }

    public async Task InvalidateAllForUserAsync(User user)
    {
        var activeLoginSessions = user.DiscordLoginSessions.Where(s => s.InvalidatedAt == null).ToList();
        // Invalidate all active LoginSessions for the user
        if (activeLoginSessions.Count > 0)
        {
            foreach (var session in activeLoginSessions)
            {
                session.InvalidatedAt = DateTime.UtcNow;
                await discordLoginSessionRepository.PersistAsync(session);
                // Invalidate all login sessions at Discord / revoke all tokens for the current user
                await discordClient.InvalidateTokenAsync(session.AccessToken);
            }
        }
    }
}