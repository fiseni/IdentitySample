using IdentitySample.Identity.Setup.Authentication.Tokens;
using Microsoft.Extensions.Caching.Memory;

namespace IdentitySample.Identity.Setup.Authentication;

public class TokenCacheMemory : ITokenCache
{
    private readonly IMemoryCache _memoryCache;
    private readonly TimeSpan _cacheDuration;

    public TokenCacheMemory(IMemoryCache memoryCache, TokenOptions tokenOptions)
    {
        _memoryCache = memoryCache;
        _cacheDuration = TimeSpan.FromMinutes(tokenOptions.RefreshTokenExpiration);
    }
    
    public Task<RefreshTokenEntry?> GetTokenAsync(string refreshToken)
    {
        var token = _memoryCache.Get(refreshToken) as RefreshTokenEntry;

        return Task.FromResult(token);
    }

    public Task RemoveTokenAsync(string refreshToken)
    {
        _memoryCache.Remove(refreshToken);

        return Task.CompletedTask;
    }

    public Task StoreTokenAsync(RefreshTokenEntry refreshTokenEntry)
    {
        _memoryCache.Remove(refreshTokenEntry.RefreshToken.Token);
        _memoryCache.Set(refreshTokenEntry.RefreshToken.Token, refreshTokenEntry, new MemoryCacheEntryOptions
        {
            SlidingExpiration = _cacheDuration,
            AbsoluteExpirationRelativeToNow = _cacheDuration,
        });

        return Task.CompletedTask;
    }
}
