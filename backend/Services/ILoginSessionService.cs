using backend.Entities;

namespace backend.Services;

public interface ILoginSessionService
{
    Task<LoginSession> CreateAsync(User user);
    Task PersistAsync(LoginSession session);
    Task<LoginSession> RefreshAsync(LoginSession session);
    Task<LoginSession?> FindActiveForUserAsync(User user);
    Task<LoginSession?> FindByRefreshToken(string refreshToken);
    Task<IEnumerable<LoginSession>> FindAllExpiredForUserAsync(User user);
    Task InvalidateAsync(LoginSession session);
}