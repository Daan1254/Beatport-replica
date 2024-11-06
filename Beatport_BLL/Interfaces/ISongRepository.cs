
using Beatport_BLL.Models.Dtos;

namespace Beatport_BLL.Interfaces;

public interface ISongRepository
{
    public List<SongDto> GetAllSongs();
    public SongDto CreateSong(CreateEditSongDto createEditSongDto);
}