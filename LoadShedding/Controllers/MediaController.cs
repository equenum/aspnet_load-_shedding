using System;
using Farfetch.LoadShedding.AspNetCore.Attributes;
using Farfetch.LoadShedding.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LoadShedding.Api;

[ApiController]
[Route("api/public/[controller]")]
public class MediaController : ControllerBase
{
    [HttpGet("stream")]
    [EndpointPriority(Priority.Critical)]
    public IActionResult GetMediaStream()
    {
        var bytes = new byte[100000];
        
        // generate random bytes to imitate a workload
        var random = new Random();
        random.NextBytes(bytes);

        return Ok(string.Join(" ", bytes));
    }
}
