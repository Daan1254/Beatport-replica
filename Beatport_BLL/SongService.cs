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

    public SongDto CreateSong(CreateEditSongDto createEditSongDto)
    {
        return _songRepository.CreateSong(createEditSongDto);
    }
}