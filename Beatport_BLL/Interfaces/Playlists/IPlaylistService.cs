using Beatport_BLL.Models.Dtos;

namespace Beatport_BLL.Interfaces;

public interface IPlaylistService
{
    public List<PlaylistDto> GetAllPlaylists();
    public PlaylistWithSongsDto? GetPlaylist(int id);
    
    public void DeleteSongFromPlaylist(AddRemoveSongFromPlaylistDto addRemoveSongFromPlaylistDto);
    
    public void AddSongToPlaylist(AddRemoveSongFromPlaylistDto addRemoveSongFromPlaylistDto);
}