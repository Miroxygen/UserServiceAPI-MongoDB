using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Route("[controller]")]
public class WayController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get()
    {
        return "You found your way here!";
    }
}
