using rate_limit_by_user_access_level.Abstractions;
using rate_limit_by_user_access_level.Domain;
using rate_limit_by_user_access_level.Dto;

namespace rate_limit_by_user_access_level.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User GetUserByEmail(string email)
    {
        return _userRepository.GetByEmailAsync(email);
    }

    public List<UserDto> GetAll()
    {
        return _userRepository.GetAll().Select(u => new UserDto(u)).ToList();
        
    }
}
