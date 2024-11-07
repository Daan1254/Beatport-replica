namespace Beatport_UI.Models;

public class HomeViewModel
{
    public SongViewModel FeaturedSong { get; set; }
    public List<SongViewModel> Songs { get; set; }
}