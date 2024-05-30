using backend.Entities;

namespace backend.Services;

public interface ILoginSessionService
{
    Task<LoginSession> Create(User user, string token);
}