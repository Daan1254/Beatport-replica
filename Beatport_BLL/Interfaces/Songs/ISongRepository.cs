using Beatport_BLL.Models.Dtos;

namespace Beatport_BLL.Interfaces;

public interface ISongRepository
{
    public List<SongDto> GetAllSongs(int? userId);
    public SongDto? GetSong(int id, int? userId);
    public bool CreateSong(CreateEditSongDto createEditSongDto);
    public bool EditSong(int id, CreateEditSongDto createEditSongDto);
    public bool DeleteSong(int id);
    public Task<int> GetTotalSongsByUser(int userId);
}