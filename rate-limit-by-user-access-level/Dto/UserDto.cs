using rate_limit_by_user_access_level.Domain;

namespace rate_limit_by_user_access_level.Dto
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string AccessLevel { get; set; }

        public UserDto(User user)
        {
            Email = user.Email;
            Password = user.Password;
            AccessLevel = Enum.GetName(typeof(AccessLevel), user.AccessLevel);
        }
    }
}
