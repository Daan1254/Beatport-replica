using Beatport_BBL.Interfaces;
using Beatport_UI.Models;

namespace Beatport_BBL;

public class SongService
{
    private readonly ISongRepository _songRepository;

    public SongService(ISongRepository songRepository)
    {
        _songRepository = songRepository;
    }

    public List<SongModel> GetAllSongs()
    {
        return _songRepository.GetAllSongs();
    }
}