namespace Beatport_BLL.Models.Dtos;

public class CreateEditSongDto
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int Bpm { get; set; }
    public string FilePath { get; set; }
}