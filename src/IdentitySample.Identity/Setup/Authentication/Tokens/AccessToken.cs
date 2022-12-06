namespace IdentitySample.Identity.Setup.Authentication.Tokens;

public class AccessToken : JsonWebToken
{
    public AccessToken(string token, long expiration) : base(token, expiration)
    {
    }
}
