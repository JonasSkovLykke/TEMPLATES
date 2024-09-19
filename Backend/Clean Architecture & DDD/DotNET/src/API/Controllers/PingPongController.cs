using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller for handling ping-pong requests.
/// </summary>
[AllowAnonymous]
[Route("[controller]")]
public class PingPongController : BaseController
{
    /// <summary>
    /// Responds to a ping request with a pong.
    /// </summary>
    /// <returns>A string "Pong" as the response to a ping request.</returns>
    /// <response code="200">A string "Pong" as the response to a ping request.</response>
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult Ping()
    {
        return Ok("Pong");
    }
}
