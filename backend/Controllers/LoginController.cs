using backend.Bodies;
using backend.Client;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController(
    IDiscordClient discordClient,
    IUserService userService,
    IDiscordLoginSessionService discordLoginSessionService,
    ILoginSessionService loginSessionService,
    IJwtService jwtService) : Controller
{
    [AllowAnonymous]
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
        await discordLoginSessionService.InvalidateAllForUserAsync(user);
        await discordLoginSessionService.CreateAsync(user, accessTokenResponse);
        // After the DiscordLoginSession is created, create a JWT token and save this to the database
        // This will also be saved in the LoginSession entity
        // TODO: add check if the user has a login session that is still valid.
        //  user has session: check if it's valid -> otherwise invalidate existing session and create a new one
        //  user has no session: create session and create new tokens
        var token = await jwtService.GenerateTokenForUserAsync(user);
        var tokenString = await jwtService.ToString(token);
        var loginSession = await loginSessionService.Create(user, tokenString);

        var response = JsonConvert.SerializeObject(new
        {
            access_token = loginSession.AccessToken,
            refresh_token = loginSession.RefreshToken
        });
        Console.WriteLine(loginSession.AccessToken);
        Console.WriteLine(loginSession.RefreshToken);

        // Return the token
        return Results.Ok(response);
    }
}