using IdentitySample.Identity.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IdentitySample.Api.Controllers;

[ApiController]
public class TestController : ControllerBase
{
    [HttpGet("/claims")]
    [SwaggerOperation(Summary = "Print claims", Tags = new[] { "Test" })]
    public ActionResult Claims(CancellationToken cancellationToken)
    {
        if (User is not null)
        {
            return Ok(User.Claims.Select(x=>x.ToString()).ToList());
        }

        return Ok("No claims");
    }

    [HttpGet("/test-read")]
    [Authorize(Policy = "TestRead")]
    [SwaggerOperation(Summary = "Test with read permission", Tags = new[] { "Test" })]
    public ActionResult Test1(CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpGet("/test-write")]
    [Authorize(Policy = "TestWrite")]
    [SwaggerOperation(Summary = "Test with write permission", Tags = new[] { "Test" })]
    public ActionResult Test2(CancellationToken cancellationToken)
    {
        return Ok();
    }
}
