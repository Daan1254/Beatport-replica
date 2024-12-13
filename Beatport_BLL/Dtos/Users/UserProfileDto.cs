namespace Beatport_BLL.Models.Dtos;

public class UserProfileDto
{
    public string Email { get; set; }
    public int TotalSongs { get; set; }
    public int TotalPlaylists { get; set; }
    public DateTime JoinDate { get; set; }
} 