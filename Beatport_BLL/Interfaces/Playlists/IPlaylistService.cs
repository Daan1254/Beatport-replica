using Beatport_BLL.Models.Dtos;

namespace Beatport_BLL.Interfaces;

public interface IPlaylistService
{
    public List<PlaylistDto> GetAllPlaylists(int? userId);
    public PlaylistWithSongsDto? GetPlaylist(int id, int? userId);
    
    public void DeleteSongFromPlaylist(AddRemoveSongFromPlaylistDto addRemoveSongFromPlaylistDto);
    
    public void AddSongToPlaylist(AddRemoveSongFromPlaylistDto addRemoveSongFromPlaylistDto);
}