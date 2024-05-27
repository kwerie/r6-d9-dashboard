using backend.Bodies;
using backend.Client;
using backend.Services;
using backend.Setup;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController(
    IDiscordClient discordClient,
    IUserService userService,
    IDiscordLoginSessionService discordLoginSessionService,
    DashboardDbContext context) : Controller
{
    [HttpPost("/api/auth/discord/login")]
    public async Task<IResult> Post([FromBody] LoginBody loginBody)
    {
        // Fetch an access_token, refresh_token, expires_in etc. from discord
        // Create DiscordLoginSession for user
        // Create LoginSession for user
        var accessTokenResponse = await discordClient.Authorize(loginBody.Code);
        if (accessTokenResponse is null)
        {
            return Results.Problem(statusCode: 401, detail: "Something went wrong");
        }

        var userInfo = await discordClient.GetUserInformation(accessTokenResponse.AccessToken);
        if (userInfo is null)
        {
            return Results.Problem(statusCode: 401, detail: "Something went wrong");
        }

        var user = await userService.FindOrCreateAsync(userInfo);
        await userService.UpdateUserInfoAsync(user, userInfo);
        // TODO: add a check if the accessToken and refreshToken do not match. If they match, DO NOT invalidate the session.
        await discordLoginSessionService.InvalidateAllForUserAsync(user);
        await discordLoginSessionService.CreateAsync(user, accessTokenResponse);

        return Results.NoContent();
    }
}