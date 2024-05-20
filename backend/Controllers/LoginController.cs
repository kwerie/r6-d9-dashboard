using backend.Bodies;
using backend.Client;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController(IDiscordClient discordClient) : Controller
{
    [HttpPost("/api/auth/discord/login")]
    public async Task<IActionResult> Post([FromBody] LoginBody loginBody)
    {
        var token = await discordClient.GetDiscordAccessToken(loginBody.Code);
        var res = await token.Content.ReadAsStringAsync();
        Console.WriteLine(res);
        return Ok(loginBody.Code);
    }
}