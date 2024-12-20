using Beatport_BLL.Models.Dtos;

public interface IUserService
{
    Task<bool> RegisterUser(RegisterUserDto registerUserDto);
    Task<bool> LoginUser(LoginUserDto loginUserDto);
    Task<UserProfileDto> GetUserProfile(string email);
} 