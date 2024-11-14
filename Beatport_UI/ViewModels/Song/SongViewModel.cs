using System.ComponentModel.DataAnnotations;

namespace Beatport_UI.Models;

public class SongViewModel
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [StringLength(60, MinimumLength = 3)]
    public string Title { get; set; }
    
    [Required]
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    public string Genre { get; set; }
    
    [Required]
    [RegularExpression("^[1-9]\\d*$")]
    public int Bpm { get; set; }
    
    [Required]
    // [FileExtensions(Extensions = "audio/mpeg, audio/wav, audio/mp3", ErrorMessage = "Please upload a valid .mp3 or .wav file")]
    public IFormFile File { get; set; }
}