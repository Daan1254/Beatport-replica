using Beatport_BLL.Interfaces;
using Beatport_BLL.Models.Dtos;
using dotenv.net;
using MySql.Data.MySqlClient;

namespace Beatport_DAL;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly string connectionStr;
    
    public PlaylistRepository()
    {
        connectionStr = DotEnv.Read()["DEFAULT_CONNECTION"];
    }
    
    public List<PlaylistDto> GetAllPlaylists()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionStr))
        {
            connection.Open();
            
            using (MySqlCommand command = new MySqlCommand("SELECT * FROM Playlists", connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    List<PlaylistDto> playlistDtos = new List<PlaylistDto>();
                    
                    while (reader.Read())
                    {
                        PlaylistDto playlistDto = new PlaylistDto
                        {
                            Id = reader.GetInt32("Id"),
                            Title = reader.GetString("Title"),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? (string?)null : reader.GetString("Description"),
                        };
                        Console.WriteLine(playlistDto.Title); 
                        playlistDtos.Add(playlistDto);
                    }
                    
                    return playlistDtos;
                }
            }
        }
    }
}