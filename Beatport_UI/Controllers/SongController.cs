using Beatport_BLL;
using Beatport_BLL.Exceptions;
using Beatport_BLL.Interfaces;
using Beatport_BLL.Models.Dtos;
using Beatport_UI.Models;
using Beatport_UI.Models.Playlist;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Beatport_UI.Controllers;

public class SongController : Controller
{
    
    private readonly ISongService _songService;
    private readonly IPlaylistService _playlistService;
    
    public SongController(ISongService songService, IPlaylistService playlistService)
    {
        _songService = songService;
        _playlistService = playlistService;
    }
    
    // GET
    [Authorize]
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
                FilePath = dto.FilePath
            }).ToList();
        
            return View(songViewModels);
        }
        catch (SongServiceException ex)
        {
            ViewData["Error"] = ex.Message;
            return View();
        }
    }
    
    
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost] 
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(SongViewModel songViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(songViewModel);
        }

        try
        {
            // Handle file upload
            string fileName = $"{Guid.NewGuid()}_{songViewModel.Title}{Path.GetExtension(songViewModel.SongFile.FileName)}";
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            
            // Create directory if it doesn't exist
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string filePath = Path.Combine(uploadsFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await songViewModel.SongFile.CopyToAsync(fileStream);
            }

            CreateEditSongDto createEditSongDto = new CreateEditSongDto
            {
                Title = songViewModel.Title,
                Genre = songViewModel.Genre,
                Bpm = songViewModel.Bpm,
                FilePath = $"/uploads/{fileName}" // Store the relative path
            };
            
            _songService.CreateSong(createEditSongDto);
            
            return RedirectToAction("Index", "Home");
        } 
        catch (SongServiceException ex)
        {
            ViewData["Error"] = ex.Message;
            return View();
        }
    }
    
    [Authorize]
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

    [Authorize]
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
    
    [Authorize]
    public IActionResult ConnectToPlaylist(int id)
    {
        try
        {
            SongDto songDto = _songService.GetSong(id);
            List<PlaylistDto> playlistDtos = _playlistService.GetAllPlaylists();
            
            List<PlaylistViewModel> playlistViewModels = playlistDtos.Select(dto => new PlaylistViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
            }).ToList();
            
            ConnectToPlaylistViewModel connectToPlaylistViewModel = new ConnectToPlaylistViewModel
            {
                Id = songDto.Id,
                Title = songDto.Title,
                Genre = songDto.Genre,
                Bpm = songDto.Bpm,
                Playlists = playlistViewModels,
            };
            
            return View(connectToPlaylistViewModel);
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
    
    [HttpPost]
    public ActionResult ConnectToPlaylist(ConnectToPlaylistViewModel connectToPlaylistViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(connectToPlaylistViewModel);
        }
        
        try
        {
            AddRemoveSongFromPlaylistDto addRemoveSongFromPlaylistDto = new AddRemoveSongFromPlaylistDto
            {
                SongId = connectToPlaylistViewModel.Id,
                PlaylistId = connectToPlaylistViewModel.SelectedPlaylistId
            };
            
            _playlistService.AddSongToPlaylist(addRemoveSongFromPlaylistDto);
            
            return RedirectToAction("Details", "Playlist", new { id = connectToPlaylistViewModel.SelectedPlaylistId });
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (PlaylistServiceException ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Index");
        }
    }
}