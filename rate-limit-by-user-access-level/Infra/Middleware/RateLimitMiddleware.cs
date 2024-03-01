using rate_limit_by_user_access_level.Abstractions;
using rate_limit_by_user_access_level.Domain;
using System.Security.Claims;

namespace rate_limit_by_user_access_level.Infra.Middleware;

public class RateLimitMiddleware
{
    private readonly RequestDelegate _next;        
    private readonly ICacheService _cacheService;

    public RateLimitMiddleware(RequestDelegate next, ICacheService cacheService)
    {
        _next = next;        
        _cacheService = cacheService;            
    }

    public async Task InvokeAsync(HttpContext context, IRateLimitService _rateLimitService)
    {
        var user = context.User;

        if (user.Identity.IsAuthenticated)
        {
            var userRole = user.FindFirst(ClaimTypes.Role)?.Value;
            var userEmail = user.FindFirst(ClaimTypes.Email)?.Value;

            var cacheKey = $"user:{userEmail}";
            var rateLimitForRole = await _cacheService.GetAsync<RateLimit>(cacheKey);

            if(rateLimitForRole is null)
            {                    
                rateLimitForRole =_rateLimitService.GetRateLimitForProfile(userRole);
            }

            var rateLimitExceeded = !_rateLimitService.IsWithinRateLimit(rateLimitForRole);
            if (rateLimitExceeded)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded.");
                return;
            }

            _rateLimitService.TrackRequest(rateLimitForRole);

            await _cacheService.SetAsync(cacheKey, rateLimitForRole, TimeSpan.FromMinutes(60));
        }

        await _next(context);
    }
}
