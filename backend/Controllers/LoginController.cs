using backend.Bodies;
using backend.Client;
using backend.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController(IDiscordClient discordClient) : Controller
{
    [HttpPost("/api/auth/discord/login")]
    public async Task<IActionResult> Post([FromBody] LoginBody loginBody)
    {
        // Fetch an access_token, refresh_token, expires_in etc. from discord
        // Create DiscordLoginSession for user
        // Create LoginSession for user
        var token = await discordClient.GetDiscordAccessToken(loginBody.Code);
        // Fetch user information from Discord
        var userInfo = await discordClient.GetUserInformation(token.AccessToken);
        // Create or update DiscordUserInformation in database
        return Ok(loginBody.Code);
    }
}