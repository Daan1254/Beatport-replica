namespace Beatport_BLL.Models.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
} 