namespace IdentitySample.Identity.Setup.Authentication.Tokens;

public abstract class JsonWebToken
{
    public string Token { get; protected set; }
    public long Expiration { get; protected set; }

    public JsonWebToken(string token, long expiration)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new Exception("Invalid token.");

        if (expiration <= 0)
            throw new Exception("Invalid expiration");

        Token = token;
        Expiration = expiration;
    }

    public bool IsExpired() => DateTime.UtcNow.Ticks > Expiration;
}
