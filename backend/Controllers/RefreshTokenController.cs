using backend.Bodies;
using backend.Entities;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class RefreshTokenController(
    ILoginSessionService loginSessionService,
    IJwtService jwtService
) : Controller
{
    [AllowAnonymous]
    [HttpPost("/api/auth/refresh-token")]
    public async Task<IResult> Post([FromBody] RefreshTokenBody requestBody)
    {
        var loginSession = await loginSessionService.FindByRefreshToken(requestBody.RefreshToken);

        if (loginSession is null)
        {
            return Results.Unauthorized();
        }

        var refreshTokenExpiresAt = loginSession.IssuedAt;
        refreshTokenExpiresAt = refreshTokenExpiresAt.AddDays(LoginSession.RefreshTokenExpiresInDays);
        if (refreshTokenExpiresAt < DateTime.Now)
        {
            return Results.Unauthorized();
        }

        loginSession.Refresh();
        await loginSessionService.PersistAsync(loginSession);
        var token = await jwtService.GenerateTokenForUserAsync(loginSession.User, loginSession.RefreshToken);
        var tokenString = await jwtService.ToString(token);
        loginSession.AccessToken = tokenString;
        await loginSessionService.PersistAsync(loginSession);

        var refreshedSessionExpiresAt = loginSession.IssuedAt;
        refreshedSessionExpiresAt = refreshedSessionExpiresAt.AddDays(LoginSession.RefreshTokenExpiresInDays);


        return Results.Ok(
            new
            {
                access_token = loginSession.AccessToken,
                refresh_token = loginSession.RefreshToken,
                refresh_token_expires_at = refreshedSessionExpiresAt
            }
        );
    }
}