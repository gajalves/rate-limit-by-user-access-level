using rate_limit_by_user_access_level.Abstractions;
using rate_limit_by_user_access_level.Domain;

namespace rate_limit_by_user_access_level.Implementations;

public class RateLimitService : IRateLimitService
{
    private Dictionary<AccessLevel, RateLimit> _rateLimits;

    public RateLimitService()
    {        
        _rateLimits = new Dictionary<AccessLevel, RateLimit>
        {
            { AccessLevel.Free, new RateLimit { Limit = 5, Period = TimeSpan.FromMinutes(5) } },
            { AccessLevel.Basic, new RateLimit { Limit = 10, Period = TimeSpan.FromMinutes(5) } },
            { AccessLevel.Pro, new RateLimit { Limit = 15, Period = TimeSpan.FromMinutes(5) } },
            { AccessLevel.Enterprise, new RateLimit { Limit = 20, Period = TimeSpan.FromMinutes(5) } }
        };
    }

    public RateLimit GetRateLimitForProfile(string profile)
    {
        var level = Enum.Parse<AccessLevel>(profile);
        return _rateLimits.TryGetValue(level, out var rateLimit) ? rateLimit : _rateLimits[AccessLevel.Free];
    }
    
    public bool IsWithinRateLimit(RateLimit ratelimit)
    {
        if ((DateTime.UtcNow - ratelimit.LastRequestTime) > ratelimit.Period)
        {
            return true;
        }

        if (ratelimit.RequestCount >= ratelimit.Limit)
        {
            return false;
        }

        return true;
    }

    public void TrackRequest(RateLimit ratelimit)
    {
        if ((DateTime.UtcNow - ratelimit.LastRequestTime) > ratelimit.Period)
        {
            ratelimit.RequestCount = 1;
            ratelimit.LastRequestTime = DateTime.UtcNow;
            return;
        }

        ratelimit.RequestCount++;
        ratelimit.LastRequestTime = DateTime.UtcNow;
    }          
}
