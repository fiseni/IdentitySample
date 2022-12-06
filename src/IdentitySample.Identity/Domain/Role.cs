﻿using Microsoft.AspNetCore.Identity;

namespace IdentitySample.Identity.Domain;

public class Role : IdentityRole<Guid>
{
    public Role(string roleName) : base(roleName)
    {
    }

    public IEnumerable<RolePermission> RolePermissions => _rolePermissions.AsEnumerable();
    private readonly List<RolePermission> _rolePermissions = new();

    public void ClearAndAddPermissions(IEnumerable<Permission> permissions)
    {
        if (permissions is null) return;

        _rolePermissions.Clear();

        _rolePermissions.AddRange(permissions.Where(x => x.Value).Select(x => new RolePermission(x)));
    }
}