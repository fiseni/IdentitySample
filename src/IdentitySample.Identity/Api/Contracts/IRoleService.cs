using IdentitySample.Identity.Api.Models;

namespace IdentitySample.Identity.Api.Contracts;

public interface IRoleService
{
    Task<RoleDto> CreateRoleAsync(RoleCreateDto roleCreateDto, CancellationToken cancellationToken);
    Task DeleteRoleAsync(Guid id, CancellationToken cancellationToken);
    Task<RoleDto> GetRoleAsync(Guid id, CancellationToken cancellationToken);
    Task<List<RoleNameDto>> GetRoleNamesAsync(CancellationToken cancellationToken);
    Task<List<RoleDto>> GetRolesAsync(CancellationToken cancellationToken);
    Task<RoleDto> UpdateRoleAsync(Guid id, RoleUpdateDto roleUpdateDto, CancellationToken cancellationToken);
}
