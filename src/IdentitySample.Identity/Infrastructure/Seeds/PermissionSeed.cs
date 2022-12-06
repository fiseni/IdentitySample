using IdentitySample.Identity.Domain;

namespace IdentitySample.Identity.Infrastructure.Seeds;

public static class PermissionSeed
{
    public static List<Permission> GeneratePermissionsForAdmin()
    {
        var permissions = new List<Permission>();

        foreach (var permissionKey in Enum.GetValues(typeof(PermissionKey)).Cast<PermissionKey>())
        {
            permissions.Add(new Permission(permissionKey, true));
        }

        return permissions;
    }
}
