using IdentitySample.Identity.Api.Models;
using IdentitySample.Identity.Setup.Authentication.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace IdentitySample.Identity.Setup.Authentication;

public class TokenHandler : ITokenHandler
{
    private readonly TokenOptions _tokenOptions;
    private readonly SigningConfigurations _signingConfigurations;
    private readonly ITokenCache _tokenCache;

    public TokenHandler(TokenOptions tokenOptions,
                        SigningConfigurations signingConfigurations,
                        ITokenCache tokenCache)
    {
        _tokenOptions = tokenOptions;
        _signingConfigurations = signingConfigurations;
        _tokenCache = tokenCache;
    }

    public async Task<TokenResponse> CreateTokensAsync(UserDto user)
    {
        var refreshToken = BuildRefreshToken();
        var accessToken = BuildAccessToken(user);

        var refreshTokenEntry = new RefreshTokenEntry(refreshToken, user.Id, user.Email);

        await _tokenCache.StoreTokenAsync(refreshTokenEntry);

        return new TokenResponse(accessToken, refreshToken);
    }

    public async Task<RefreshTokenEntry?> TakeRefreshTokenAsync(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken)) return null;

        var token = await _tokenCache.GetTokenAsync(refreshToken);
        await _tokenCache.RemoveTokenAsync(refreshToken);

        return token;
    }

    public async Task RevokeRefreshTokenAsync(string refreshToken)
    {
        await _tokenCache.RemoveTokenAsync(refreshToken);
    }

    private RefreshToken BuildRefreshToken()
    {
        var refreshToken = new RefreshToken
        (
            token: HashString(Guid.NewGuid().ToString()),
            expiration: DateTime.UtcNow.AddMinutes(_tokenOptions.RefreshTokenExpiration).Ticks
        );

        return refreshToken;
    }

    private AccessToken BuildAccessToken(UserDto user)
    {
        var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);

        var securityToken = new JwtSecurityToken
        (
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            claims: GetClaims(user),
            expires: accessTokenExpiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: _signingConfigurations.SigningCredentials
        );

        var handler = new JwtSecurityTokenHandler();
        var accessToken = handler.WriteToken(securityToken);

        return new AccessToken(accessToken, accessTokenExpiration.Ticks);
    }

    private static IEnumerable<Claim> GetClaims(UserDto user)
    {
        var claims = new List<Claim>();
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()));

        if (user.FirstName is not null)
            claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName));

        if (user.LastName is not null)
            claims.Add(new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName));

        if (user.Email is not null)
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString()));

        if (user.Roles.Any())
            claims.Add(new Claim("roles", string.Join(' ', user.Roles.Select(x => x.Name))));

        return claims;
    }

    private static string HashString(string value)
    {
        byte[] salt;
        byte[] buffer2;

        using (var bytes = new Rfc2898DeriveBytes(value, 0x10, 0x3e8))
        {
            salt = bytes.Salt;
            buffer2 = bytes.GetBytes(0x20);
        }
        var dst = new byte[0x31];
        Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
        Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
        return Convert.ToBase64String(dst);
    }
}
