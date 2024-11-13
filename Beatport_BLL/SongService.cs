using Beatport_BLL.Exceptions;
using Beatport_BLL.Interfaces;
using Beatport_BLL.Models.Dtos;

namespace Beatport_BLL;

public class SongService
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
        catch (Exception ex)
        {
            throw new BadRequestException("An error occurred while fetching songs.", ex);
        }
    }
    
    public SongDto GetSong(int id)
    {
        try
        {
            SongDto? song = _songRepository.GetSong(id);

            if (song == null)
            {
                throw new SongNotFoundException(id);
            }
        
            return song;
        }
        catch (Exception ex)
        {
            throw new BadRequestException("An error occurred while fetching song.", ex);
        }
        
    }

    public bool CreateSong(CreateEditSongDto createEditSongDto)
    {
        try
        {
            return _songRepository.CreateSong(createEditSongDto);
        }
        catch (Exception ex)
        {
            throw new BadRequestException("An error occurred while creating song.", ex);
        }
    }

    public bool EditSong(int id, CreateEditSongDto createEditSongDto)
    {
        try
        {
            SongDto? song = _songRepository.GetSong(id);

            if (song == null)
            {
                throw new SongNotFoundException(id);
            }
        
            return _songRepository.EditSong(id, createEditSongDto);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while editing song.", ex);
        }
    }
    
    public bool DeleteSong(int id)
    {
        try
        {
            SongDto? song = _songRepository.GetSong(id);

            if (song == null)
            {
                throw new SongNotFoundException(id);
            }
        
            // TODO: Add Soft Delete and check if author is the same
        
            return _songRepository.DeleteSong(id);
        }
        catch (Exception ex)
        {
            throw new BadRequestException("An error occurred while deleting song.", ex);
        }
        
    }
}