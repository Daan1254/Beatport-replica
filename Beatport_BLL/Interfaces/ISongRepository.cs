using Beatport_UI.Models;
using Beatport_UI.Models.Dtos;

namespace Beatport_BLL.Interfaces;

public interface ISongRepository
{
    public List<SongDto> GetAllSongs();
}