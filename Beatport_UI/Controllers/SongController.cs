using Beatport_BBL;
using Beatport_UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Beatport_UI.Controllers;

public class SongController : Controller
{
    
    private readonly SongService _songService;
    
    public SongController(SongService songService)
    {
        _songService = songService;
    }
    
    // GET
    public IActionResult Index()
    {
        List<SongModel> songs = _songService.GetAllSongs();
        
        return View(songs);
    }
    
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost] 
    public ActionResult CreateSong(SongModel sm)
    {
 
        return View("Index");
    }
}