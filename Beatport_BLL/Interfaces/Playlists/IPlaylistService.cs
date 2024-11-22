using Beatport_BLL.Models.Dtos;

namespace Beatport_BLL.Interfaces;

public interface IPlaylistService
{
    public List<PlaylistDto> GetAllPlaylists();
    public PlaylistDto? GetPlaylist(int id);
}