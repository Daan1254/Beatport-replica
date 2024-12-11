using Beatport_BLL.Exceptions;
using Beatport_BLL.Interfaces;
using Beatport_BLL.Models.Dtos;

namespace Beatport_BLL;

public class SongService : ISongService
{
    private readonly ISongRepository _songRepository;

    public SongService(ISongRepository songRepository)
    {
        _songRepository = songRepository;
    }

    public List<SongDto> GetAllSongs()
    {
        try
        {
            return _songRepository.GetAllSongs();
        }
        catch (SongRepositoryException ex)
        {
            throw new SongServiceException("An error occurred while fetching songs.", ex);
        }
    }
    
    public SongDto GetSong(int id)
    {
        try
        {
            SongDto? song = _songRepository.GetSong(id);

            if (song == null)
            {
                throw new NotFoundException($"Song with id {id} not found");
            }
        
            return song;
        }
        catch (SongRepositoryException ex)
        {
            throw new SongServiceException("An error occurred while fetching song.", ex);
        }
        
    }

    public bool CreateSong(CreateEditSongDto createEditSongDto)
    {
        try
        {
            return _songRepository.CreateSong(createEditSongDto);
        }
        catch (SongRepositoryException ex)
        {
            throw new SongServiceException("An error occurred while creating song.", ex);
        }
    }

    public bool EditSong(int id, CreateEditSongDto createEditSongDto)
    {
        try
        {
            SongDto? song = _songRepository.GetSong(id);

            if (song == null)
            {
                throw new NotFoundException($"Song with id {id} not found");
            }
        
            return _songRepository.EditSong(id, createEditSongDto);
        }
        catch (SongRepositoryException ex)
        {
            throw new SongServiceException("An error occurred while editing song.", ex);
        }
    }
    
    public bool DeleteSong(int id)
    {
        try
        {
            SongDto? song = _songRepository.GetSong(id);

            if (song == null)
            {
                throw new NotFoundException($"Song with id {id} not found");
            }
        
            // TODO: Add Soft Delete and check if author is the same
        
            return _songRepository.DeleteSong(id);
        }
        catch (SongRepositoryException ex)
        {
            throw new SongServiceException("An error occurred while deleting song.", ex);
        }
    }
}