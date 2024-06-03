using System.Security.Claims;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace backend.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class GetUserController(
    IUserService userService
) : Controller
{
    [HttpGet("/api/auth/user")]
    public async Task<IResult> Get()
    {
        var identity = HttpContext.User;
        var claims = identity.Claims;
        var userId = int.Parse(claims.First(c => c.Type == "user_id").Value);
        var user = await userService.FindByIdAsync(userId);
        if (user is null)
        {
            return Results.BadRequest();
        }

        return Results.Ok(
            JsonConvert.SerializeObject(
                new
                {
                    username = user.Username,
                    email = user.Email,
                    avatar_url = user.AvatarUrl
                }
            )
        );
    }
}