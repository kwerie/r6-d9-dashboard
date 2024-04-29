using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : Controller
{
    [HttpPost("/api/login")]
    public IActionResult Post([FromBody] LoginBody loginBody)
    {
        return Ok(loginBody.Code);
    }
}