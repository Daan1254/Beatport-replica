using System.ComponentModel.DataAnnotations;

namespace Beatport_UI.Models.Playlist;

public class PlaylistViewModel
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [MinLength(60)] 
    public string Title { get; set; }
    
    public string? Description { get; set; }
}