using Beatport_BLL;
using Beatport_UI.Models;
using Beatport_UI.Models.Dtos;
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
        List<SongDto> songDtos = _songService.GetAllSongs();
        
         List<SongViewModel> songViewModels = songDtos.Select(dto => new SongViewModel
        {
            Id = dto.Id,
            Title = dto.Title,
            Genre = dto.Genre,
            Bpm = dto.Bpm,
        }).ToList();
        
        return View(songViewModels);
    }
    
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost] 
    public ActionResult CreateSong(SongViewModel sm)
    {
 
        return View("Index");
    }
}