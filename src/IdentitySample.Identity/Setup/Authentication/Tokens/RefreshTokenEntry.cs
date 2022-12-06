namespace IdentitySample.Identity.Setup.Authentication.Tokens;

public class RefreshTokenEntry
{
    public RefreshToken RefreshToken { get; }
    public Guid UserId { get; }
    public string Email { get; }

    public RefreshTokenEntry(RefreshToken refreshToken, Guid userId, string email)
    {
        RefreshToken = refreshToken;
        UserId = userId;
        Email = email;
    }
}
