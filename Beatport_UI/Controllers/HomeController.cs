using System.Diagnostics;
using Beatport_BLL.Interfaces;
using Beatport_BLL.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Beatport_UI.Models;

namespace Beatport_UI.Controllers;

public class HomeController : Controller
{
    private readonly ISongService _songService;

    public HomeController(ISongService songService)
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
            
            HomeViewModel homeViewModel = new HomeViewModel
            {
                FeaturedSong = songViewModels.Select(s => s).FirstOrDefault() ?? new SongViewModel(),
                Songs = songViewModels.Where(s => s.Id != songViewModels.Select(s => s).FirstOrDefault().Id).ToList()
            };
       
            return View(homeViewModel);
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