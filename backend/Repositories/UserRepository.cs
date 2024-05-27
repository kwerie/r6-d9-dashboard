using backend.Entities;
using backend.Responses;
using backend.Setup;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class UserRepository(DashboardDbContext context) : IUserRepository
{
    public async Task<User> FindOrCreate(UserInformationResponse userInfo)
    {
        var existingUser = await context.Users.Include(u => u.DiscordLoginSessions)
            .FirstOrDefaultAsync(u => u.Email == userInfo.Email);

        if (existingUser is not null)
        {
            return existingUser;
        }

        var user = new User
        {
            Email = userInfo.Email,
            Username = userInfo.Username,
            AvatarUrl = userInfo.GetAvatarUrl,
            DiscordAccountId = userInfo.Id,
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateUserInfoAsync(User user, UserInformationResponse userInfo)
    {
        user.Username = userInfo.Username;
        user.DiscordAccountId = userInfo.Id;
        user.AvatarUrl = userInfo.GetAvatarUrl;
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }
}