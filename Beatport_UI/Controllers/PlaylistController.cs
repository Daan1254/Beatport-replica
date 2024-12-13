using Beatport_BLL.Exceptions;
using Beatport_BLL.Interfaces;
using Beatport_BLL.Models.Dtos;
using Beatport_UI.Models;
using Beatport_UI.Models.Playlist;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Beatport_UI.Controllers;

[Authorize]
public class PlaylistController : Controller
{
    private readonly IPlaylistService _playlistService;
    
    public PlaylistController(IPlaylistService playlistService)
    {
        _playlistService = playlistService;
    }
    
    // GET
    [Authorize]
    public IActionResult Index()
    {
        int? userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        }

        List<PlaylistDto> playlistDtos = _playlistService.GetAllPlaylists(userId);
        
        List<PlaylistViewModel> playlistViewModels = playlistDtos.Select(dto => new PlaylistViewModel
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
        }).ToList();
        
        return View(playlistViewModels);
    }
    
    [Authorize]
    public IActionResult Details(int id)
    {
        try
        {
            int? userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            PlaylistWithSongsDto? playlistDto = _playlistService.GetPlaylist(id, userId);
        
            if (playlistDto == null)
            {
                return NotFound();
            }
        
            PlaylistWithSongsViewModel playlistWithSongsViewModel = new PlaylistWithSongsViewModel
            {
                Id = playlistDto.Id,
                Title = playlistDto.Title,
                Description = playlistDto.Description,
                Songs = playlistDto.Songs.Select(songDto => new SongViewModel
                {
                    Id = songDto.Id,
                    Title = songDto.Title,
                    Genre = songDto.Genre,
                    Bpm = songDto.Bpm,
                }).ToList() 
            };
        
            return View(playlistWithSongsViewModel);
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
    
    [HttpPost]
    public IActionResult DeleteSongFromPlaylist(int songId, int playlistId)
    {
        AddRemoveSongFromPlaylistDto addRemoveSongFromPlaylistDto = new AddRemoveSongFromPlaylistDto
        {
            SongId = songId,
            PlaylistId = playlistId,
        };
        
        try
        {
            _playlistService.DeleteSongFromPlaylist(addRemoveSongFromPlaylistDto);
            return RedirectToAction("Details", new { id = playlistId });
        }
        catch (NotFoundException ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Details", new { id = playlistId });
        }
        catch (PlaylistServiceException ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Index");
        }
    }
}