using System.Diagnostics;
using Beatport_BBL;
using Microsoft.AspNetCore.Mvc;
using Beatport_UI.Models;

namespace Beatport_UI.Controllers;

public class HomeController : Controller
{
    private readonly SongService _songService;

    public HomeController(SongService songService)
    {
        _songService = songService;
    }
    
    public ActionResult Index()
    {
       List<SongModel> songs = _songService.GetAllSongs();
       
        return View(songs);
    }
    
    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}