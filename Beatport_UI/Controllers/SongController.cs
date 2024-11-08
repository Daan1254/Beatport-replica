using Beatport_BLL;
using Beatport_BLL.Models.Dtos;
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
    [ValidateAntiForgeryToken]
    public ActionResult Create(SongViewModel songViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(songViewModel);
            }
        
            CreateEditSongDto createEditSongDto = new CreateEditSongDto
            {
                Title = songViewModel.Title,
                Genre = songViewModel.Genre,
                Bpm = songViewModel.Bpm
            };
            
            _songService.CreateSong(createEditSongDto);
            
            return RedirectToAction("Index", "Home");

        } catch (Exception e)
        {
            ViewData["Error"] = "An error occurred";
            return View();
        }
    }
}