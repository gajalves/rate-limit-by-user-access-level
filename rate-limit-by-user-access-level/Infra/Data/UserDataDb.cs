using rate_limit_by_user_access_level.Domain;

namespace rate_limit_by_user_access_level.Infra.Data;

public static class UserDataDb
{
    public static List<User> Users = new List<User>
    {
        new User { Id = 1, Email = "free@mail.com", Password = "123", AccessLevel = AccessLevel.Free},
        new User { Id = 2, Email = "basic@mail.com", Password = "123", AccessLevel = AccessLevel.Basic},
        new User { Id = 3, Email = "pro@mail.com", Password = "123", AccessLevel = AccessLevel.Pro},
        new User { Id = 4, Email = "enterprise@mail.com", Password = "123", AccessLevel = AccessLevel.Enterprise}
    };
}