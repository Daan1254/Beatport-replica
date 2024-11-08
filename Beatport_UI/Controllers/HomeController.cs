using System.Diagnostics;
using Beatport_BLL;
using Beatport_BLL.Models.Dtos;
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
        try
        {
            List<SongDto> songDtos = _songService.GetAllSongs();
       
            List<SongViewModel> songViewModels = songDtos.Select(dto => new SongViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Genre = dto.Genre,
                Bpm = dto.Bpm,
            }).ToList();
       
            return View(songViewModels);
        } catch (Exception e)
        {
            ViewData["Error"] = "An error occurred";
            return View();
        }
       
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