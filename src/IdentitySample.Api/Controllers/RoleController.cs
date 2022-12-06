using IdentitySample.Identity.Api.Contracts;
using IdentitySample.Identity.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IdentitySample.Api.Controllers;

[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get role", Tags = new[] { "Role" })]
    public async Task<ActionResult<RoleDto>> Get(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _roleService.GetRoleAsync(id, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get roles", Tags = new[] { "Role" })]
    public async Task<ActionResult<List<RoleNameDto>>> List(CancellationToken cancellationToken = default)
    {
        var result = await _roleService.GetRolesAsync(cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create role", Tags = new[] { "Role" })]
    public async Task<ActionResult<RoleDto>> Create([FromBody] RoleCreateDto role,
        CancellationToken cancellationToken = default)
    {
        var result = await _roleService.CreateRoleAsync(role, cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update role", Tags = new[] { "Role" })]
    public async Task<ActionResult<RoleDto>> Update(Guid id, [FromBody] RoleUpdateDto role,
        CancellationToken cancellationToken = default)
    {
        var result = await _roleService.UpdateRoleAsync(id, role, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete role", Tags = new[] { "Role" })]
    public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _roleService.DeleteRoleAsync(id, cancellationToken);

        return Ok();
    }
}
