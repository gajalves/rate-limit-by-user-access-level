using rate_limit_by_user_access_level.Infra.Data;

namespace rate_limit_by_user_access_level.Domain;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public AccessLevel AccessLevel { get; set; }
}
