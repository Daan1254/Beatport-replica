using System.Security.Claims;
using System.Security.Cryptography;
using Beatport_BLL.Exceptions;
using Beatport_BLL.Interfaces;
using Beatport_BLL.Models.Dtos;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Beatport_BLL;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISongRepository _songRepository;
    private readonly IPlaylistRepository _playlistRepository;

    public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, ISongRepository songRepository, IPlaylistRepository playlistRepository)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _songRepository = songRepository;
        _playlistRepository = playlistRepository;
    }

    public async Task<bool> RegisterUser(RegisterUserDto registerUserDto)
    {
        try
        {
            // Validate password match
            if (registerUserDto.Password != registerUserDto.ConfirmPassword)
            {
                throw new UserServiceException("Passwords do not match");
            }

            // Check if user already exists
            UserDto? existingUser = await _userRepository.GetUserByEmail(registerUserDto.Email);
            if (existingUser != null)
            {
                throw new UserServiceException("User with this email already exists");
            }

            // Hash password
            string passwordHash = HashPassword(registerUserDto.Password);

            // Create user
            return await _userRepository.CreateUser(registerUserDto, passwordHash);
        }
        catch (UserRepositoryException ex)
        {
            throw new UserServiceException("Failed to register user", ex);
        }
    }

    public async Task<bool> LoginUser(LoginUserDto loginUserDto)
    {
        try
        {
            UserDto? user = await _userRepository.GetUserByEmail(loginUserDto.Email);
            
            if (user == null)
            {
                throw new UserServiceException("Invalid email or password");
            }

            if (!VerifyPassword(loginUserDto.Password, user.PasswordHash))
            {
                throw new UserServiceException("Invalid email or password");
            }

            // Create claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // This will create a persistent cookie
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7) // Cookie expires in 7 days
            };
            // Sign in
            await _httpContextAccessor.HttpContext!.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return true;
        }
        catch (UserRepositoryException ex)
        {
            throw new UserServiceException("Failed to login", ex);
        }
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }

    public async Task<UserProfileDto> GetUserProfile(string email)
    {
        UserDto? user = await _userRepository.GetUserByEmail(email);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        // Get user statistics
        int totalSongs = await _songRepository.GetTotalSongsByUser(user.Id);
        int totalPlaylists = await _playlistRepository.GetTotalPlaylistsByUser(user.Id);

        return new UserProfileDto
        {
            Email = user.Email,
            TotalSongs = totalSongs,
            TotalPlaylists = totalPlaylists,
            JoinDate = user.CreatedAt
        };
    }
} 