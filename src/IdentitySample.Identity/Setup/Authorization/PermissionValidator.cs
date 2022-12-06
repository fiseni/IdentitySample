using IdentitySample.Identity.Api.Models;
using System.Collections.Concurrent;

namespace IdentitySample.Identity.Setup.Authorization;

public class PermissionValidator : IPermissionValidator
{
    public static PermissionValidator Instance { get; } = new PermissionValidator();
    private PermissionValidator() { }

    private readonly ConcurrentDictionary<string, List<int>> _roles = new();

    public bool ValidateForRoles(int requiredPermissionKey, string[] roles)
    {
        foreach (var role in roles)
        {
            if (_roles.TryGetValue(role, out var permissionKeys) &&
                permissionKeys.Contains(requiredPermissionKey))
            {
                return true;
            }
        }

        return false;
    }

    public void UpdateCache(List<RoleDto> roles)
    {
        _roles.Clear();

        foreach (var role in roles)
        {
            // Do not use AddOrUpdate and GetOrAdd methods. Delegates for these methods are called outside the locks and are not thread safe.

            _roles.TryAdd(role.Name!, role.Permissions.Where(x => x.Value).Select(x => x.Key).ToList());
        }
    }
}
