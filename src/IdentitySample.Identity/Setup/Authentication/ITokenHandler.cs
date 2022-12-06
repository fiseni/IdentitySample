using IdentitySample.Identity.Api.Models;
using IdentitySample.Identity.Setup.Authentication.Tokens;

namespace IdentitySample.Identity.Setup.Authentication;

public interface ITokenHandler
{
    Task<TokenResponse> CreateTokensAsync(UserDto user);
    Task<RefreshTokenEntry?> TakeRefreshTokenAsync(string token);
    Task RevokeRefreshTokenAsync(string token);
}
