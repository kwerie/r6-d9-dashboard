using backend.Responses;

namespace backend.Client;

public interface IDiscordClient
{
    Task<AccessTokenResponse> GetDiscordAccessToken(string code);
    Task<UserInformationResponse> GetUserInformation(string accessToken);
}