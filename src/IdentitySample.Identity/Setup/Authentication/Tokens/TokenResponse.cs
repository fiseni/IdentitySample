namespace IdentitySample.Identity.Setup.Authentication.Tokens;

public class TokenResponse
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }

    public TokenResponse(AccessToken accessToken, RefreshToken refreshToken)
    {
        ArgumentNullException.ThrowIfNull(accessToken);
        ArgumentNullException.ThrowIfNull(refreshToken);

        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}
