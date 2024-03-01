using rate_limit_by_user_access_level.Domain;

namespace rate_limit_by_user_access_level.Abstractions;

public interface ITokenGenerator
{
    string GenerateToken(User user);
}
