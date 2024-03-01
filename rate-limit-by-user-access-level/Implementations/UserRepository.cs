using rate_limit_by_user_access_level.Abstractions;
using rate_limit_by_user_access_level.Domain;
using rate_limit_by_user_access_level.Infra.Data;

namespace rate_limit_by_user_access_level.Implementations;

public class UserRepository : IUserRepository
{
    public User GetByEmailAsync(string email)
    {
        return UserDataDb.Users.Where(u => u.Email == email).FirstOrDefault();
    }

    public List<User> GetAll()
    {
        return UserDataDb.Users.ToList();
    }
}
