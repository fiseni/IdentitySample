using Microsoft.AspNetCore.Identity;

namespace IdentitySample.Identity.Domain;

public class UserRole : IdentityUserRole<Guid>
{
    public User User { get; private set; } = default!;
    public Role Role { get; private set; } = default!;

    public UserRole(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}
