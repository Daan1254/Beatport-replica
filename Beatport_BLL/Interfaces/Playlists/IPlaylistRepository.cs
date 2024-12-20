using Beatport_BLL.Models.Dtos;

namespace Beatport_BLL.Interfaces;

public interface IPlaylistRepository
{
    public List<PlaylistDto> GetAllPlaylists(int? userId);
    public PlaylistWithSongsDto? GetPlaylist(int id, int? userId);
    public void DeleteSongFromPlaylist(AddRemoveSongFromPlaylistDto addRemoveSongFromPlaylistDto);
    public void AddSongToPlaylist(AddRemoveSongFromPlaylistDto addRemoveSongFromPlaylistDto);
    public bool CreatePlaylist(CreateEditPlaylistDto createEditPlaylistDto);
    public bool EditPlaylist(int id, CreateEditPlaylistDto createEditPlaylistDto);
    public bool DeletePlaylist(int id);
    public Task<int> GetTotalPlaylistsByUser(int userId);
}