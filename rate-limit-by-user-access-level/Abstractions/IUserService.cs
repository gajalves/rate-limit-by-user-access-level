using rate_limit_by_user_access_level.Domain;
using rate_limit_by_user_access_level.Dto;

namespace rate_limit_by_user_access_level.Abstractions;

public interface IUserService
{
    User GetUserByEmail(string email);
    List<UserDto> GetAll();
}
