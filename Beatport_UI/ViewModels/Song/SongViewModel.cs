using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Beatport_UI.Models;

public class SongViewModel
{
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Genre { get; set; }
    
    [Required]
    [Range(1, 999)]
    public int Bpm { get; set; }
    
    public string? FilePath { get; set; }
}