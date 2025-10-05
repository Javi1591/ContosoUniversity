using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Controllers;

[ApiController]
[Route("status")]
public class StatusController : ControllerBase
{
    private readonly ILogger<StatusController> _logger;

    private readonly IWebHostEnvironment _env;

    public StatusController(ILogger<StatusController> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    // HTTP GET: /status
    [HttpGet]
    public IActionResult Get()
    {
        // Logger for app monitoring 
        _logger.LogInformation(
            "Status check OK at {UtcNow} in {Environment}",
            DateTimeOffset.UtcNow,        // Dsiplays Current Time
            _env.EnvironmentName);        // Displays current environment

        // Return JSON result
        return Ok(new
        {
            status = "ok",                // Displays app status
            env = _env.EnvironmentName    // Displays step in pipeline
        });
    }
}
