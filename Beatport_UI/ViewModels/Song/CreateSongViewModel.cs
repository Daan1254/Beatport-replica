using System.ComponentModel.DataAnnotations;

namespace Beatport_UI.Models;

public class CreateSongViewModel : SongViewModel
{
    [Required(ErrorMessage = "Please select a song file")]
    public IFormFile SongFile { get; set; }
}