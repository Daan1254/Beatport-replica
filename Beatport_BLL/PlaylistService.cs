using Beatport_BLL.Exceptions;
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
        } catch (PlaylistRepositoryException ex)
        {
            throw new PlaylistServiceException(ex.Message);
        }
    }
    
    public PlaylistWithSongsDto? GetPlaylist(int id)
    {
        try
        {
            PlaylistWithSongsDto? playlistDto =  _playlistRepository.GetPlaylist(id);
            
            if (playlistDto == null)
            {
                throw new NotFoundException($"Playlist with id {id} not found");
            }
            
            return playlistDto;
        } catch (PlaylistRepositoryException ex)
        {
            throw new PlaylistServiceException(ex.Message);
        }
    }
    
    public void DeleteSongFromPlaylist(AddRemoveSongFromPlaylistDto addRemoveSongFromPlaylistDto)
    {
        try
        {
            _playlistRepository.DeleteSongFromPlaylist(addRemoveSongFromPlaylistDto);
        } catch (PlaylistRepositoryException ex)
        {
            throw new PlaylistServiceException(ex.Message);
        }
    }
}