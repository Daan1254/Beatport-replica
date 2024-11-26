using Beatport_BLL.Exceptions;
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
        using MySqlConnection connection = new MySqlConnection(connectionStr);
        using MySqlCommand command = new MySqlCommand("SELECT * FROM Playlists", connection);

        try
        {
            connection.Open();

            using MySqlDataReader reader = command.ExecuteReader();
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
        catch (MySqlException ex)
        {
            throw new PlaylistRepositoryException("An error occurred while fetching playlists.", ex);
        }
    }
    
    public PlaylistDto? GetPlaylist(int id)
    {
        using MySqlConnection connection = new MySqlConnection(connectionStr);
        using MySqlCommand command = new MySqlCommand($@"
            SELECT 
                p.Id AS PlaylistId,
                p.Title AS PlaylistTitle,
                p.Description AS PlaylistDescription,
                s.Id AS SongId,
                s.Title AS SongTitle,
                s.Bpm AS SongBpm,
                s.Genre AS SongGenre,
            FROM 
                Playlists p
            LEFT JOIN 
                Playlist_Songs ps ON p.Id = ps.PlaylistId
            LEFT JOIN 
                Songs s ON ps.SongId = s.Id
            WHERE 
                p.Id = @Id;
        ", connection);
        command.Parameters.AddWithValue("@Id", id);
        
        try
        {
            connection.Open();

            using MySqlDataReader reader = command.ExecuteReader();
            PlaylistDto? playlistDto = null;
            List<SongDto> songs = new List<SongDto>();

            while (reader.Read())
            {
                // Initialize PlaylistDto only once
                playlistDto ??= new PlaylistWithSongsDto()
                {
                    Id = reader.GetInt32("PlaylistId"),
                    Title = reader.GetString("PlaylistTitle"),
                    Description = reader.IsDBNull(reader.GetOrdinal("PlaylistDescription"))
                        ? null
                        : reader.GetString("PlaylistDescription"),
                    Songs = songs // Link the songs list to the playlist
                };

                // Add a song to the songs list if present
                if (!reader.IsDBNull(reader.GetOrdinal("SongId")))
                {
                    songs.Add(new SongDto
                    {
                        Id = reader.GetInt32("SongId"),
                        Title = reader.GetString("SongTitle"),
                        Bpm = reader.GetInt32("Bpm"),
                        Genre = reader.GetString("Genre"),
                        // Artist = reader.IsDBNull(reader.GetOrdinal("SongArtist")) ? null : reader.GetString("SongArtist"),
                    });
                }
            }
            return playlistDto;
        }
        catch (MySqlException ex)
        {
            throw new PlaylistRepositoryException("An error occurred while fetching playlist.", ex);
        }
    }
}