using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Beatport_UI.Models;
using dotenv.net;
using MySql.Data.MySqlClient;

namespace Beatport_UI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly string connectionStr = DotEnv.Read()["DEFAULT_CONNECTION"];

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<SongModel> songs = new List<SongModel>();
        using (MySqlConnection mySqlConnection = new MySqlConnection(connectionStr))
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM songs", mySqlConnection);
            
            mySqlConnection.Open();
            
            MySqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                songs.Add(new SongModel
                {
                    Id = reader.GetInt32("id"),
                    Title = reader.GetString("title"),
                    Genre = reader.GetString("genre"),
                    Bpm = reader.GetInt32("bpm")
                });
            }
        }
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