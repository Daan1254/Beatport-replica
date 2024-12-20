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
    
    public List<PlaylistDto> GetAllPlaylists(int? userId)
    {
        try
        {
            return _playlistRepository.GetAllPlaylists(userId);
        } catch (PlaylistRepositoryException ex)
        {
            throw new PlaylistServiceException(ex.Message);
        }
    }
    
    public PlaylistWithSongsDto? GetPlaylist(int id, int? userId)
    {
        try
        {
            PlaylistWithSongsDto? playlistDto =  _playlistRepository.GetPlaylist(id, userId);
            
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

    public void CreatePlaylist(CreateEditPlaylistDto createEditPlaylistDto)
    {
        try
        {
            _playlistRepository.CreatePlaylist(createEditPlaylistDto);
        } catch (PlaylistRepositoryException ex)
        {
            throw new PlaylistServiceException(ex.Message);
        }
    }

    public void EditPlaylist(int id, CreateEditPlaylistDto createEditPlaylistDto)
    {
        try
        {
            _playlistRepository.EditPlaylist(id, createEditPlaylistDto);
        } catch (PlaylistRepositoryException ex)
        {
            throw new PlaylistServiceException(ex.Message);
        }
    }

    public void DeletePlaylist(int id)
    {
        try
        {
            _playlistRepository.DeletePlaylist(id);
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
    
    public void AddSongToPlaylist(AddRemoveSongFromPlaylistDto addRemoveSongFromPlaylistDto)
    {
        try
        {
            _playlistRepository.AddSongToPlaylist(addRemoveSongFromPlaylistDto);
        } catch (PlaylistRepositoryException ex)
        {
            throw new PlaylistServiceException(ex.Message);
        }
    }
}