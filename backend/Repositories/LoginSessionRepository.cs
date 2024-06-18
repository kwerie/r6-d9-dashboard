using backend.Entities;
using backend.Setup;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class LoginSessionRepository(DashboardDbContext context) : ILoginSessionRepository
{
    public async Task<LoginSession> CreateAsync(User user)
    {
        var session = new LoginSession
        {
            UserId = user.Id
        };
        context.LoginSessions.Add(session);
        await context.SaveChangesAsync();
        return session;
    }

    public async Task PersistAsync(LoginSession session)
    {
        context.LoginSessions.Update(session);
        await context.SaveChangesAsync();
    }

    public async Task<LoginSession> RefreshAsync(LoginSession session)
    {
        session.Refresh();
        context.LoginSessions.Update(session);
        await context.SaveChangesAsync();
        return session;
    }

    public Task<LoginSession?> FindActiveForUserAsync(User user)
    {
        return Task.FromResult(user.LoginSessions.FirstOrDefault(
            s => s.InvalidatedAt is null && s.ExpiresAt >= DateTime.Now
        ));
    }

    public Task<IEnumerable<LoginSession>> FindAllExpiredForUserAsync(User user)
    {
        return Task.FromResult(
            context.LoginSessions
                .Where(l => l.User == user)
                .ToList()
                .Where(
                    l => l.InvalidatedAt == null && l.ExpiresAt < DateTime.Now
                ).AsEnumerable()
        );
    }

    public async Task<LoginSession?> FindByRefreshToken(string refreshToken)
    {
        return await context.LoginSessions.Include(ls => ls.User).FirstOrDefaultAsync(ls => ls.RefreshToken == refreshToken && ls.InvalidatedAt == null);
    }

    public async Task InvalidateAsync(LoginSession session)
    {
        session.InvalidatedAt = DateTime.Now;
        context.LoginSessions.Update(session);
        await context.SaveChangesAsync();
    }
}