using Beatport_BLL;
using Beatport_BLL.Exceptions;
using Beatport_BLL.Interfaces;
using Beatport_BLL.Models.Dtos;
using Beatport_UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Beatport_UI.Controllers;

public class SongController : Controller
{
    
    private readonly ISongService _songService;
    
    public SongController(ISongService songService)
    {
        _songService = songService;
    }
    
    // GET
    public IActionResult Index()
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
        }
        catch (SongServiceException ex)
        {
            ViewData["Error"] = ex.Message;
            return View();
        }
    }
    
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost] 
    [ValidateAntiForgeryToken]
    public ActionResult Create(SongViewModel songViewModel)
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
        
        try
        {
            _songService.CreateSong(createEditSongDto);
            
            return RedirectToAction("Index", "Home");
        } 
        catch (SongServiceException ex)
        {
            ViewData["Error"] = ex.Message;
            return View();
        }
    }
    
    public IActionResult Edit(int Id)
    {
        try
        {
            SongDto songDto = _songService.GetSong(Id);

            SongViewModel songViewModel = new SongViewModel
            {
                Id = songDto.Id,
                Title = songDto.Title,
                Genre = songDto.Genre,
                Bpm = songDto.Bpm
            };

            return View(songViewModel);
        }
        catch (NotFoundException ex)
        {
            return NotFound();
        }
        catch (SongServiceException ex)
        {
            ViewData["Error"] = ex.Message;
            return View();
        }
    }
    
    [HttpPost] 
    [ValidateAntiForgeryToken]
    public ActionResult Edit(SongViewModel songViewModel)
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
        
        try
        {
            _songService.EditSong(songViewModel.Id, createEditSongDto);
            
            return RedirectToAction("Index");
        } 
        catch(NotFoundException)
        {
            return NotFound();
        }
        catch (SongServiceException ex)
        {
            ViewData["Error"] = ex.Message;
            return View();
        }
    }
    
    public IActionResult Delete(int Id)
    {
        try
        {
            SongDto songDto = _songService.GetSong(Id);
            
            DeleteSongViewModel songViewModel = new DeleteSongViewModel()
            {
                Id = songDto.Id,
                Title = songDto.Title,
            };
        
            return View(songViewModel);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (SongServiceException ex)
        {
            ViewData["Error"] = ex.Message;
            return View();
        }
    }
    
    
    [HttpPost]
    public ActionResult DeleteSong(int Id)
    {
        try
        {
            _songService.DeleteSong(Id);
            return RedirectToAction("Index");
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (SongServiceException ex)
        {
            ViewData["Error"] = ex.Message;
            return RedirectToAction("Index");
        }
    }
}