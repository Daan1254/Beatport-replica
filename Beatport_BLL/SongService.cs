using Beatport_BLL.Interfaces;
using Beatport_UI.Models;

namespace Beatport_BLL;

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