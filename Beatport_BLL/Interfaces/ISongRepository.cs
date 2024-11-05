using Beatport_UI.Models;

namespace Beatport_BBL.Interfaces;

public interface ISongRepository
{
    public List<SongModel> GetAllSongs();
}