using Beatport_BLL.Interfaces;
using Beatport_BLL.Models.Dtos;

namespace Beatport_BLL;

public class PlaylistService : IPlaylistService
{
    private readonly IPlaylistRepository _playlistRepository;
    
    public PlaylistService(IPlaylistRepository playlistRepository)
    {
        _playlistRepository = playlistRepository;
    }
    
    public List<PlaylistDto> GetAllPlaylists()
    {
        try
        {
            return _playlistRepository.GetAllPlaylists();
        } catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    public PlaylistDto? GetPlaylist(int id)
    {
        try
        {
            return _playlistRepository.GetPlaylist(id);
        } catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}