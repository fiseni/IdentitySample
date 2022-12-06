namespace IdentitySample.Identity.Setup.Authentication.Tokens;

public class RefreshToken : JsonWebToken
{
    public RefreshToken(string token, long expiration) : base(token, expiration)
    {
    }
}
