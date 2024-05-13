using System.Threading.Tasks;
using Farfetch.LoadShedding.AspNetCore.Attributes;
using Farfetch.LoadShedding.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LoadShedding.Api;

[ApiController]
[Route("api/public/[controller]")]
public class UserPreferencesController : ControllerBase
{
    [HttpGet("lang")]
    [EndpointPriority(Priority.NonCritical)]
    public async Task<IActionResult> GetLanguageAsync()
    {
        await Task.Delay(1000);
        return Ok("English");
    }
}
