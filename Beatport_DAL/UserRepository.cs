using Beatport_BLL.Exceptions;
using Beatport_BLL.Models.Dtos;
using dotenv.net;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

public class UserRepository : IUserRepository
{
    private readonly string _connectionStr;
    
    public UserRepository()
    {
        _connectionStr = DotEnv.Read()["DEFAULT_CONNECTION"];
    }

    public async Task<bool> CreateUser(RegisterUserDto registerUserDto, string passwordHash)
    {
        using MySqlConnection connection = new MySqlConnection(_connectionStr);
        using MySqlCommand command = new MySqlCommand(@"
            INSERT INTO Users (Email, PasswordHash, CreatedAt)
            VALUES (@Email, @PasswordHash, @CreatedAt)", connection);

        command.Parameters.AddWithValue("@Email", registerUserDto.Email);
        command.Parameters.AddWithValue("@PasswordHash", passwordHash);
        command.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);

        try
        {
            await connection.OpenAsync();
            return await command.ExecuteNonQueryAsync() > 0;
        }
        catch (MySqlException ex)
        {
            throw new UserRepositoryException("Error creating user", ex);
        }
    }

    public async Task<UserDto?> GetUserByEmail(string email)
    {
        try
        {
            using MySqlConnection connection = new MySqlConnection(_connectionStr);
            using MySqlCommand command = new MySqlCommand(
                "SELECT * FROM Users WHERE Email = @Email", connection);
            
            command.Parameters.AddWithValue("@Email", email);

            await connection.OpenAsync();
            using MySqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                return new UserDto
                {
                    Id = reader.GetInt32("Id"),
                    Email = reader.GetString("Email"),
                    PasswordHash = reader.GetString("PasswordHash"),
                    CreatedAt = reader.GetDateTime("CreatedAt")
                };
            }
            return null;
        }
        catch (MySqlException ex)
        {
            throw new UserRepositoryException("Failed to fetch user", ex);
        }
    }

    public async Task<bool> ValidateCredentials(string email, string password)
    {
        var user = await GetUserByEmail(email);
        if (user == null) return false;
        
        return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
    }

    public async Task<int> GetTotalSongsByUser(int userId)
    {
        using MySqlConnection connection = new MySqlConnection(_connectionStr);
        using MySqlCommand command = new MySqlCommand(
            "SELECT COUNT(*) FROM songs WHERE user_id = @UserId AND deleted_at IS NULL", 
            connection);
        
        command.Parameters.AddWithValue("@UserId", userId);
        
        try
        {
            await connection.OpenAsync();
            return Convert.ToInt32(await command.ExecuteScalarAsync());
        }
        catch (MySqlException ex)
        {
            throw new UserRepositoryException("Error getting total songs", ex);
        }
    }

    public async Task<int> GetTotalPlaylistsByUser(int userId)
    {
        using MySqlConnection connection = new MySqlConnection(_connectionStr);
        using MySqlCommand command = new MySqlCommand(
            "SELECT COUNT(*) FROM playlists WHERE user_id = @UserId AND deleted_at IS NULL", 
            connection);
        
        command.Parameters.AddWithValue("@UserId", userId);
        
        try
        {
            await connection.OpenAsync();
            return Convert.ToInt32(await command.ExecuteScalarAsync());
        }
        catch (MySqlException ex)
        {
            throw new UserRepositoryException("Error getting total playlists", ex);
        }
    }
} 