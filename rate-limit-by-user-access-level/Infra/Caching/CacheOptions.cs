using Microsoft.Extensions.Caching.Distributed;

namespace rate_limit_by_user_access_level.Infra.Caching;

public static class CacheOptions
{
    public static DistributedCacheEntryOptions DefaultExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
    };

    public static DistributedCacheEntryOptions Create(TimeSpan? expiration) =>
        expiration is not null ?
        new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration } :
        DefaultExpiration;
}
