namespace backend.Client;

public interface IDiscordClient
{
    Task<HttpResponseMessage> GetDiscordAccessToken(string code);
}