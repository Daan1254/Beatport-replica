using Beatport_UI.Models;

namespace Beatport_BLL.Interfaces;

public interface ISongRepository
{
    public List<SongModel> GetAllSongs();
}