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

    public Task UpdateUserInfoAsync(User user, UserInformationResponse userInfo)
    {
        return userRepository.UpdateUserInfoAsync(user, userInfo);
    }

    public Task<User?> FindByIdAsync(int id)
    {
        return userRepository.FindByIdAsync(id);
    }
}