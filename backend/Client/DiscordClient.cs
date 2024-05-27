using System.Net;
using System.Net.Http.Headers;
using backend.Extensions;
using backend.Responses;

namespace backend.Client;

public class DiscordClient(
    HttpClient client,
    string clientId,
    string clientSecret,
    string redirectUri
) : IDiscordClient
{
    private const string DiscordApiBaseUrl = "https://discord.com/api";
    private const string AccessTokenEndpoint = "/oauth2/token";
    private const string RevokeTokenEndpoint = "/oauth2/token/revoke";
    private const string UserInformationEndpoint = "/users/@me";

    public async Task<AccessTokenResponse?> Authorize(string code)
    {
        var formContent = new FormUrlEncodedContent(
            new[]
            {
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("redirect_uri", redirectUri),
            }
        );

        var response = await client.PostAsync(
            $"{DiscordApiBaseUrl}{AccessTokenEndpoint}",
            formContent
        );
        if (response.StatusCode != HttpStatusCode.OK)
        {
            return null;
        }

        return await response.ConvertIntoObject<AccessTokenResponse>();
    }

    public async Task<UserInformationResponse?> GetUserInformation(string accessToken)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await client.GetAsync(
            $"{DiscordApiBaseUrl}{UserInformationEndpoint}"
        );
        if (response.StatusCode != HttpStatusCode.OK)
        {
            return null;
        }

        return await response.ConvertIntoObject<UserInformationResponse>();
    }

    public async Task InvalidateTokenAsync(string token)
    {
        var formContent = new FormUrlEncodedContent(
            new[]
            {
                new KeyValuePair<string, string>("token", token),
                new KeyValuePair<string, string>("token_type_hint", "access_token"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
            }
        );
        var response = await client.PostAsync(
            $"{DiscordApiBaseUrl}{RevokeTokenEndpoint}",
            formContent
        );
    }
}