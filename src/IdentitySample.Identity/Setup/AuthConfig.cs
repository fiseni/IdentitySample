using IdentitySample.Identity.Setup.Authentication;
using Microsoft.Extensions.Configuration;

namespace IdentitySample.Identity.Setup;

public class AuthConfig
{
    public const string CONFIG_NAME = "Auth";

    public static AuthConfig Instance { get; } = new AuthConfig();
    private AuthConfig() { }

    public string Audience { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public long AccessTokenExpiration { get; set; }
    public long RefreshTokenExpiration { get; set; }
    public string Secret { get; set; } = default!;

    public TokenOptions GetTokenOptions(IConfiguration configuration) => new TokenOptions
    {
        Audience = Audience,
        Issuer = Issuer,
        AccessTokenExpiration = AccessTokenExpiration,
        RefreshTokenExpiration = RefreshTokenExpiration,
        Secret = Secret,
    };
}
