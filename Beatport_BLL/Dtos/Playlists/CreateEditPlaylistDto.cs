namespace Beatport_BLL.Models.Dtos;

public class CreateEditPlaylistDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public int UserId { get; set; }
} 