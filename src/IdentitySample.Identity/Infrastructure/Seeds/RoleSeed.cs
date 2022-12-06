using IdentitySample.Identity.Domain;

namespace IdentitySample.Identity.Infrastructure.Seeds;

public static class RoleSeed
{
    public static readonly string AdminRole = "Admin";

    public static List<Role> GetRoles()
    {
        var permissions = PermissionSeed.GeneratePermissionsForAdmin();
        var adminRole = new Role(AdminRole);

        adminRole.ClearAndAddPermissions(permissions);

        return new List<Role>
        {
            adminRole,
            new("PowerUsers"),
            new("StandardUsers")
        };
    }
}
