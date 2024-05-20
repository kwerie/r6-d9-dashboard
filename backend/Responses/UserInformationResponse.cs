using Newtonsoft.Json;

namespace backend.Responses;

public class UserInformationResponse
{
    [JsonProperty("id")]
    public required string Id { get; init; }

    [JsonProperty("username")]
    public required string Username { get; init; }

    [JsonProperty("avatar")]
    public required string AvatarId { get; init; }

    [JsonProperty("email")]
    public required string Email { get; init; }

    public string GetAvatarUrl => $"https://cdn.discordapp.com/avatars/{Id}/{AvatarId}.jpg";
}