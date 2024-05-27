using backend.Responses;

namespace backend.Client;

public interface IDiscordClient
{
    Task<AccessTokenResponse?> Authorize(string code);
    Task<UserInformationResponse?> GetUserInformation(string accessToken);
    Task InvalidateTokenAsync(string token);
}