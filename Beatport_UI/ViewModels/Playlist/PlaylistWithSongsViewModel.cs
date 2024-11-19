using Beatport_BLL.Models.Dtos;

namespace Beatport_UI.Models.Playlist;

public class PlaylistWithSongsViewModel : PlaylistViewModel
{
    public List<SongDto> Songs { get; set; }
}