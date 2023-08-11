using Microsoft.AspNetCore.Mvc;

namespace ChatAI.API.Controllers;

[Route("/")]
[ApiController]
public sealed class APIVersionController : ControllerBase
{
    public APIVersionController()
    {
    }

    [HttpGet]
    public IActionResult GetVersion()
    {
        return Ok(new { version = "0.2.0" });
    }
}
