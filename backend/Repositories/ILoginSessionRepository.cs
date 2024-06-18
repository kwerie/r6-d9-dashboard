using backend.Entities;

namespace backend.Repositories;

public interface ILoginSessionRepository
{
    Task<LoginSession> CreateAsync(User user);
    Task PersistAsync(LoginSession session);
    Task<LoginSession> RefreshAsync(LoginSession session);
    Task<LoginSession?> FindActiveForUserAsync(User user);
    Task<IEnumerable<LoginSession>> FindAllExpiredForUserAsync(User user);
    Task InvalidateAsync(LoginSession session);
    Task<LoginSession?> FindByRefreshToken(string refreshToken);
}