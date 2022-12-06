using Microsoft.AspNetCore.Authorization;

namespace IdentitySample.Identity.Setup.Authorization;

public static class ScopeAuthorizationRequirementExtensions
{
    public static AuthorizationPolicyBuilder RequireAppScope(
        this AuthorizationPolicyBuilder authorizationPolicyBuilder,
        params string[] requiredScopes)
    {
        authorizationPolicyBuilder.RequireAppScope((IEnumerable<string>)requiredScopes);
        return authorizationPolicyBuilder;
    }

    public static AuthorizationPolicyBuilder RequireAppScope(
        this AuthorizationPolicyBuilder authorizationPolicyBuilder,
        IEnumerable<string> requiredScopes)
    {
        authorizationPolicyBuilder.AddRequirements(new ScopeAuthorizationRequirement(requiredScopes));
        return authorizationPolicyBuilder;
    }
}
