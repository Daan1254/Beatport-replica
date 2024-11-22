namespace Beatport_BLL.Models.Dtos;

public class PlaylistWithSongsDto : PlaylistDto
{
    public List<SongDto> Songs { get; set; }
}