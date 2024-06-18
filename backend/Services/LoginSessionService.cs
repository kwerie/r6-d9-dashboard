using backend.Entities;
using backend.Repositories;

namespace backend.Services;

public class LoginSessionService(ILoginSessionRepository loginSessionRepository) : ILoginSessionService
{
    public Task<LoginSession> CreateAsync(User user)
    {
        return loginSessionRepository.CreateAsync(user);
    }

    public Task PersistAsync(LoginSession session)
    {
        return loginSessionRepository.PersistAsync(session);
    }

    public Task<LoginSession> RefreshAsync(LoginSession session)
    {
        return loginSessionRepository.RefreshAsync(session);
    }

    public Task<LoginSession?> FindActiveForUserAsync(User user)
    {
        return loginSessionRepository.FindActiveForUserAsync(user);
    }

    public Task<LoginSession?> FindByRefreshToken(string refreshToken)
    {
        return loginSessionRepository.FindByRefreshToken(refreshToken);
    }

    public Task<IEnumerable<LoginSession>> FindAllExpiredForUserAsync(User user)
    {
        return loginSessionRepository.FindAllExpiredForUserAsync(user);
    }

    public Task InvalidateAsync(LoginSession session)
    {
        return loginSessionRepository.InvalidateAsync(session);
    }
}