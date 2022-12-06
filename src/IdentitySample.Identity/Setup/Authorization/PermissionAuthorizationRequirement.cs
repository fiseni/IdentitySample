using Microsoft.AspNetCore.Authorization;

namespace IdentitySample.Identity.Setup.Authorization;

public class PermissionAuthorizationRequirement : AuthorizationHandler<PermissionAuthorizationRequirement>, IAuthorizationRequirement
{
    public int RequiredPermissionKey { get; }

    public PermissionAuthorizationRequirement(int requiredPermissionKey)
    {
        RequiredPermissionKey = requiredPermissionKey;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
    {
        if (context.User is not null)
        {
            var rolesClaim = context.User.Claims.FirstOrDefault(
                c => c.Type.Equals("roles", StringComparison.OrdinalIgnoreCase));

            if (rolesClaim is not null)
            {
                var roles = rolesClaim.Value.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (PermissionValidator.Instance.ValidateForRoles(RequiredPermissionKey, roles))
                {
                    context.Succeed(requirement);
                }
            }
        }

        return Task.CompletedTask;
    }
}
