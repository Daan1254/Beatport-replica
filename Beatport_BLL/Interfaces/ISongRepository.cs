
using Beatport_BLL.Models.Dtos;

namespace Beatport_BLL.Interfaces;

public interface ISongRepository
{
    public List<SongDto> GetAllSongs();
    public SongDto? GetSong(int id);
    public bool CreateSong(CreateEditSongDto createEditSongDto);
    public bool EditSong(int id, CreateEditSongDto createEditSongDto);
    public bool DeleteSong(int id);
}