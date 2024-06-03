using backend.Entities;
using backend.Responses;

namespace backend.Repositories;

public interface IUserRepository
{
    Task<User> FindOrCreate(UserInformationResponse userInfo);
    Task UpdateUserInfoAsync(User user, UserInformationResponse userInfo);
    Task<User?> FindByIdAsync(int id);
}