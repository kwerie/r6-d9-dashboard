using backend.Bodies;
using backend.Client;
using backend.Entities;
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
        var loginSession = await loginSessionService.FindActiveForUserAsync(user);

        var expiredSessions = await loginSessionService.FindAllExpiredForUserAsync(user);
        foreach (var session in expiredSessions)
        {
            await loginSessionService.InvalidateAsync(session);
        }

        if (loginSession is not null)
        {
            var expiresAt = loginSession.IssuedAt;
            expiresAt = expiresAt.AddDays(LoginSession.RefreshTokenExpiresInDays);
            return Results.Ok(
                new
                {
                    access_token = loginSession.AccessToken,
                    refresh_token = loginSession.RefreshToken,
                    refresh_token_expires_at = expiresAt
                }
            );
        }

        loginSession = await loginSessionService.CreateAsync(user);

        var token = await jwtService.GenerateTokenForUserAsync(user, loginSession.RefreshToken);
        var tokenString = await jwtService.ToString(token);

        loginSession.AccessToken = tokenString;
        await loginSessionService.PersistAsync(loginSession);

        var refreshTokenExpiresAt = loginSession.IssuedAt;
        refreshTokenExpiresAt = refreshTokenExpiresAt.AddDays(LoginSession.RefreshTokenExpiresInDays);

        return Results.Ok(
            new
            {
                access_token = loginSession.AccessToken,
                refresh_token = loginSession.RefreshToken,
                refresh_token_expires_at = refreshTokenExpiresAt
            }
        );
    }
}