namespace backend.Client;

public class DiscordClient(
    HttpClient client,
    string clientId,
    string clientSecret,
    string redirectUri
) : IDiscordClient
{
    private const string DISCORD_API_BASE_URL = "https://discord.com/api";
    private const string ACCESS_TOKEN_ENDPOINT = "/oauth2/token";
    private const string USER_INFORMATION_ENDPOINTS = "/users/@me";

    public async Task<HttpResponseMessage> GetDiscordAccessToken(string code)
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

        return await client.PostAsync(
            $"{DISCORD_API_BASE_URL}{ACCESS_TOKEN_ENDPOINT}",
            formContent
        );
    }
}