namespace IdentitySample.Identity.Setup.Authentication;

public class TokenOptions
{
    public string Audience { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public long AccessTokenExpiration { get; set; }
    public long RefreshTokenExpiration { get; set; }
    public string Secret { get; set; } = default!;
}
