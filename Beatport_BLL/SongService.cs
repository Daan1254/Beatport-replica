using Beatport_BLL.Interfaces;
using Beatport_UI.Models;
using Beatport_UI.Models.Dtos;

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
}