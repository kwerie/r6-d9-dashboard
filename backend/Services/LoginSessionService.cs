using backend.Entities;
using backend.Repositories;

namespace backend.Services;

public class LoginSessionService(ILoginSessionRepository loginSessionRepository) : ILoginSessionService
{
    public async Task <LoginSession> Create(User user, string token)
    {
        return await loginSessionRepository.Create(user, token);
    }
}