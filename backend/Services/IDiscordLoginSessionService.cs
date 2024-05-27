using backend.Entities;
using backend.Responses;

namespace backend.Services;

public interface IDiscordLoginSessionService
{
    Task<DiscordLoginSession> CreateAsync(User user, AccessTokenResponse accessTokenResponse);
    Task InvalidateAsync(DiscordLoginSession session);
    Task InvalidateAllForUserAsync(User user);
}