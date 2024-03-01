using rate_limit_by_user_access_level.Domain;

namespace rate_limit_by_user_access_level.Abstractions;

public interface IRateLimitService
{
    RateLimit GetRateLimitForProfile(string profile);
    bool IsWithinRateLimit(RateLimit rateLimit);
    void TrackRequest(RateLimit rateLimit);
}
