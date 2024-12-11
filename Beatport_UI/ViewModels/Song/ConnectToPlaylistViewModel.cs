using System.ComponentModel.DataAnnotations;
using Beatport_UI.Models.Playlist;

namespace Beatport_UI.Models;

public class ConnectToPlaylistViewModel : SongViewModel
{
    public List<PlaylistViewModel> Playlists { get; set; }
    
    [Required]
    public int SelectedPlaylistId { get; set; }
}