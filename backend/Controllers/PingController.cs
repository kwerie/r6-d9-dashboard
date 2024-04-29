using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class PingController : Controller
{
    [HttpGet("/api/ping")]
    public IActionResult Get()
    {
        var response = new Dictionary<string, long>
        {
            { "ack", long.Parse($"{DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds:#}") }
        };

        return Ok(response);
    }
}