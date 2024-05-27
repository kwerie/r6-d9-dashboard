using backend.Entities;
using backend.Repositories;
using backend.Responses;

namespace backend.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public Task<User> FindOrCreateAsync(UserInformationResponse userInfo)
    {
        return userRepository.FindOrCreate(userInfo);
    }

    public async Task UpdateUserInfoAsync(User user, UserInformationResponse userInfo)
    {
        await userRepository.UpdateUserInfoAsync(user, userInfo);
    }
}