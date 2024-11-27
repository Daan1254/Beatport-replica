using Beatport_BLL.Models.Dtos;

namespace Beatport_BLL.Interfaces;

public interface IPlaylistRepository
{
    public List<PlaylistDto> GetAllPlaylists();
    public PlaylistWithSongsDto? GetPlaylist(int id);
    public void DeleteSongFromPlaylist(AddRemoveSongFromPlaylistDto addRemoveSongFromPlaylistDto);
}