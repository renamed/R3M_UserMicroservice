
using Microsoft.AspNetCore.Mvc;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    public UserController()
    {

    }

    [HttpGet("{userId}")
    public async Task<IActionResult> Get(int userId)
    {

    }

    [HttpPost]

}