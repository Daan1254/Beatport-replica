namespace Beatport_BLL.Models.Dtos;

public class SongDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public int Bpm { get; set; }
    public string FilePath { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}