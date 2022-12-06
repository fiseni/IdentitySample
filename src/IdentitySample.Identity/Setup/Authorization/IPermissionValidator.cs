using IdentitySample.Identity.Api.Models;

namespace IdentitySample.Identity.Setup.Authorization;

public interface IPermissionValidator
{
    void UpdateCache(List<RoleDto> roles);
    bool ValidateForRoles(int requiredPermissionKey, string[] roles);
}
