using Microsoft.AspNetCore.Authorization;

namespace IdentitySample.Identity.Setup.Authorization;

public class ScopeAuthorizationRequirement : AuthorizationHandler<ScopeAuthorizationRequirement>, IAuthorizationRequirement
{
    public IEnumerable<string> RequiredScopes { get; }

    public ScopeAuthorizationRequirement(IEnumerable<string> requiredScopes)
    {
        RequiredScopes = requiredScopes;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ScopeAuthorizationRequirement requirement)
    {
        if (context.User is not null)
        {
            var scopeClaim = context.User.Claims.FirstOrDefault(
                c => string.Equals(c.Type, "http://schemas.microsoft.com/identity/claims/scope", StringComparison.OrdinalIgnoreCase));

            if (scopeClaim is not null)
            {
                var scopes = scopeClaim.Value.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (requirement.RequiredScopes.All(requiredScope => scopes.Contains(requiredScope)))
                {
                    context.Succeed(requirement);
                }
            }
        }

        return Task.CompletedTask;
    }
}
