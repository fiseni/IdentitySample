using IdentitySample.Identity.Api.Contracts;
using IdentitySample.Identity.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IdentitySample.Api.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get user", Tags = new[] { "User" })]
    public async Task<ActionResult<UserDto>> Get(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _userService.GetUserAsync(id, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "List users", Tags = new[] { "User" })]
    public async Task<ActionResult<UserDto>> List(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _userService.GetUserAsync(id, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create user", Tags = new[] { "User" })]
    public async Task<ActionResult<UserDto>> Create([FromBody] UserCreateDto user,
        CancellationToken cancellationToken = default)
    {
        var result = await _userService.CreateUserAsync(user, cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update user", Tags = new[] { "User" })]
    public async Task<ActionResult<UserDto>> Update(Guid id, [FromBody] UserUpdateDto user,
        CancellationToken cancellationToken = default)
    {
        var result = await _userService.UpdateUserAsync(id, user, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete user", Tags = new[] { "User" })]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        await _userService.DeleteUserAsync(id, cancellationToken);

        return Ok();
    }
}
