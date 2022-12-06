using IdentitySample.Identity.Setup.Authentication.Tokens;

namespace IdentitySample.Identity.Setup.Authentication;

public interface ITokenCache
{
    Task StoreTokenAsync(RefreshTokenEntry refreshTokenEntry);
    Task RemoveTokenAsync(string refreshToken);
    Task<RefreshTokenEntry?> GetTokenAsync(string refreshToken);
}
