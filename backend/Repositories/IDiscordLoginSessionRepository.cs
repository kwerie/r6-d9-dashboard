using backend.Entities;
using backend.Responses;

namespace backend.Repositories;

public interface IDiscordLoginSessionRepository
{
    Task<DiscordLoginSession> CreateAsync(User user, AccessTokenResponse accessTokenResponse);
    Task PersistAsync(DiscordLoginSession session);
}