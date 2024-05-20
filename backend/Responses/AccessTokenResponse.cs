using Newtonsoft.Json;

namespace backend.Responses;

public class AccessTokenResponse
{
    [JsonProperty("access_token")]
    public required string AccessToken { get; init; }

    [JsonProperty("token_type")] // Bearer
    public required string TokenType { get; init; }

    [JsonProperty("expires_in")]
    public required int ExpiresIn { get; init; }

    [JsonProperty("refresh_token")]
    public required string RefreshToken { get; init; }

    [JsonProperty("scope")]
    public required string Scope { get; init; }
}