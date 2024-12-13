using Beatport_BLL.Models.Dtos;

public interface IUserRepository
{
    Task<UserDto?> GetUserByEmail(string email);
    Task<bool> CreateUser(RegisterUserDto registerUserDto, string passwordHash);
    Task<bool> ValidateCredentials(string email, string password);
    Task<int> GetTotalSongsByUser(int userId);
    Task<int> GetTotalPlaylistsByUser(int userId);
} 