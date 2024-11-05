using Beatport_UI.Models;
using dotenv.net;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Beatport_UI.Controllers;

public class SongController : Controller
{
    
    private readonly string connectionStr = DotEnv.Read()["DEFAULT_CONNECTION"];
    // GET
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
    
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost] 
    public ActionResult CreateSong(SongModel sm)
    {
 
        return View("Index");
    }
}