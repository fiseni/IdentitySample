using IdentitySample.Identity.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IdentitySample.Api.Controllers;

[ApiController]
public class TestController : ControllerBase
{

    [HttpGet("/test-read")]
    [Authorize(Policy = nameof(PermissionKey.TestRead))]
    [SwaggerOperation(Summary = "Test with read permission", Tags = new[] { "Test" })]
    public ActionResult Test1(CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpGet("/test-write")]
    [Authorize(Policy = nameof(PermissionKey.TestRead))]
    [SwaggerOperation(Summary = "Test with write permission", Tags = new[] { "Test" })]
    public ActionResult Test2(CancellationToken cancellationToken)
    {
        return Ok();
    }
}
