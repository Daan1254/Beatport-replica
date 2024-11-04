using Beatport_UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Beatport_UI.Controllers;

public class SongController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost] 
    public ActionResult CreateSong(SongModel sm)
    {
        Console.WriteLine(sm.Title);
        Console.WriteLine(sm.Bpm);
        Console.WriteLine(sm.Genre);
 
        return View("Index");
    }
}