using Beatport_BLL.Interfaces;
using Beatport_BLL.Models.Dtos;
using Beatport_UI.Models.Playlist;
using Microsoft.AspNetCore.Mvc;

namespace Beatport_UI.Controllers;

public class PlaylistController : Controller
{
    private readonly IPlaylistService _playlistService;
    
    public PlaylistController(IPlaylistService playlistService)
    {
        _playlistService = playlistService;
    }
    
    // GET
    public IActionResult Index()
    {
        List<PlaylistDto> playlistDtos = _playlistService.GetAllPlaylists();
        
        List<PlaylistViewModel> playlistViewModels = playlistDtos.Select(dto => new PlaylistViewModel
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
        }).ToList();
        
        return View(playlistViewModels);
    }
}