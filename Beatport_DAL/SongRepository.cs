using Beatport_BLL.Interfaces;
using Beatport_UI.Models;
using dotenv.net;
using MySql.Data.MySqlClient;

namespace Beatport_DAL;

public class SongRepository : ISongRepository
{
    private readonly string connectionStr;
    
    public SongRepository()
    {
        connectionStr = DotEnv.Read()["DEFAULT_CONNECTION"];
    }
    
    public List<SongModel> GetAllSongs()
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
        return songs;
    }
}