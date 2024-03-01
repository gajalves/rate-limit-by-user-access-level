namespace rate_limit_by_user_access_level.Domain;

public class RateLimit
{
    public int Limit { get; set; }
    public TimeSpan Period { get; set; }
    public int RequestCount { get; set; } = 1;
    public DateTime LastRequestTime { get; set; }
}
