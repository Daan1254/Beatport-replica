using System.ComponentModel.DataAnnotations;

namespace Beatport_UI.Models;

public class SongViewModel
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    public string Genre { get; set; }
    
    [Required]
    [RegularExpression("^[1-9]\\d*$")]
    public int Bpm { get; set; }
}