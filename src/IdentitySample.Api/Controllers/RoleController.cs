using IdentitySample.Identity.Api.Contracts;
using IdentitySample.Identity.Api.Models;
using IdentitySample.Identity.Setup.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IdentitySample.Api.Controllers;

[ApiController]
[Route("identity/roles")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly IPermissionValidator _permissionValidator;

    public RoleController(IRoleService roleService, IPermissionValidator permissionValidator)
    {
        _roleService = roleService;
        _permissionValidator = permissionValidator;
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

        var roles = await _roleService.GetRolesAsync(CancellationToken.None);
        _permissionValidator.UpdateCache(roles);

        return Ok(result);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update role", Tags = new[] { "Role" })]
    public async Task<ActionResult<RoleDto>> Update(Guid id, [FromBody] RoleUpdateDto role,
        CancellationToken cancellationToken = default)
    {
        var result = await _roleService.UpdateRoleAsync(id, role, cancellationToken);

        var roles = await _roleService.GetRolesAsync(CancellationToken.None);
        _permissionValidator.UpdateCache(roles);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete role", Tags = new[] { "Role" })]
    public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _roleService.DeleteRoleAsync(id, cancellationToken);

        var roles = await _roleService.GetRolesAsync(CancellationToken.None);
        _permissionValidator.UpdateCache(roles);

        return Ok();
    }
}
