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
        return _songRepository.GetAllSongs();
    }
    
    public SongDto GetSong(int id)
    {
        SongDto? song = _songRepository.GetSong(id);

        if (song == null)
        {
            throw new SongNotFoundException(id);
        }
        
        return song;
    }

    public bool CreateSong(CreateEditSongDto createEditSongDto)
    {
        return _songRepository.CreateSong(createEditSongDto);
    }

    public bool EditSong(int id, CreateEditSongDto createEditSongDto)
    {
        SongDto? song = _songRepository.GetSong(id);

        if (song == null)
        {
            throw new SongNotFoundException(id);
        }
        
        return _songRepository.EditSong(id, createEditSongDto);
    }
    
    public bool DeleteSong(int id)
    {
        SongDto? song = _songRepository.GetSong(id);

        if (song == null)
        {
            throw new SongNotFoundException(id);
        }
        
        return _songRepository.DeleteSong(id);
    }
}