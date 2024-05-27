using backend.Entities;
using backend.Responses;

namespace backend.Services;

public interface IUserService
{
    Task<User> FindOrCreateAsync(UserInformationResponse userInfo);
    Task UpdateUserInfoAsync(User user, UserInformationResponse userInfo);
}