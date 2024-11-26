using Beatport_BLL.Exceptions;
using Beatport_BLL.Interfaces;
using Beatport_BLL.Models.Dtos;
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
    
    public List<SongDto> GetAllSongs()
    {
       
        List<SongDto> songs = new List<SongDto>();
        using MySqlConnection mySqlConnection = new MySqlConnection(connectionStr);
        using MySqlCommand cmd = new MySqlCommand("SELECT * FROM songs", mySqlConnection);
        
        try
        {
            mySqlConnection.Open();
            
            MySqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                songs.Add(new SongDto
                {
                    Id = reader.GetInt32("id"),
                    Title = reader.GetString("title"),
                    Genre = reader.GetString("genre"),
                    Bpm = reader.GetInt32("bpm"),
                    CreatedAt = reader.GetDateTime("CreatedAt"),
                    UpdatedAt = reader.IsDBNull(reader.GetOrdinal("UpdatedAt")) ? (DateTime?)null : reader.GetDateTime("UpdatedAt"),
                    DeletedAt = reader.IsDBNull(reader.GetOrdinal("DeletedAt")) ? (DateTime?)null : reader.GetDateTime("DeletedAt"),
                });
            }
            mySqlConnection.Close();
            return songs;
        } catch (MySqlException ex)
        {
            throw new SongRepositoryException("An error occurred while fetching songs.", ex);
        }
    }

    public SongDto? GetSong(int id)
    {
        SongDto? song = null;
        using MySqlConnection mySqlConnection = new MySqlConnection(connectionStr);
        using MySqlCommand cmd = new MySqlCommand("SELECT * FROM songs WHERE id = @id", mySqlConnection);
        cmd.Parameters.AddWithValue("@id", id);

        mySqlConnection.Open();
        try
        {
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                song = new SongDto
                {
                    Id = reader.GetInt32("id"),
                    Title = reader.GetString("title"),
                    Genre = reader.GetString("genre"),
                    Bpm = reader.GetInt32("bpm"),
                    CreatedAt = reader.GetDateTime("CreatedAt"),
                    UpdatedAt = reader.IsDBNull(reader.GetOrdinal("UpdatedAt"))
                        ? (DateTime?)null
                        : reader.GetDateTime("UpdatedAt"),
                    DeletedAt = reader.IsDBNull(reader.GetOrdinal("DeletedAt"))
                        ? (DateTime?)null
                        : reader.GetDateTime("DeletedAt"),
                };
            }

            mySqlConnection.Close();
            return song;
        }
        catch (MySqlException ex)
        {
            throw new SongRepositoryException("Something went wrong while getting song", ex);
        }
    }

    public bool CreateSong(CreateEditSongDto createEditSongDto)
    {
        using MySqlConnection mySqlConnection = new MySqlConnection(connectionStr);
        using MySqlCommand cmd = new MySqlCommand("INSERT INTO songs (title, genre, bpm) VALUES (@title, @genre, @bpm)", mySqlConnection);
        cmd.Parameters.AddWithValue("@title", createEditSongDto.Title);
        cmd.Parameters.AddWithValue("@genre", createEditSongDto.Genre);
        cmd.Parameters.AddWithValue("@bpm", createEditSongDto.Bpm);
        try
        {
            mySqlConnection.Open();
            
            return cmd.ExecuteNonQuery() > 0;
        }
        catch (MySqlException ex)
        {
            throw new SongRepositoryException("An error occurred while creating song.", ex);
        }
        
    }
    
    
    public bool EditSong(int id, CreateEditSongDto createEditSongDto)
    {
        using MySqlConnection mySqlConnection = new MySqlConnection(connectionStr);
        using MySqlCommand cmd = new MySqlCommand("UPDATE songs SET title = @title, genre = @genre, bpm = @bpm WHERE id = @id", mySqlConnection);
        cmd.Parameters.AddWithValue("@title", createEditSongDto.Title);
        cmd.Parameters.AddWithValue("@genre", createEditSongDto.Genre);
        cmd.Parameters.AddWithValue("@bpm", createEditSongDto.Bpm);
        cmd.Parameters.AddWithValue("@id", id);
        
        try
        {
            mySqlConnection.Open();

            return cmd.ExecuteNonQuery() > 0;
        } catch (MySqlException ex)
        {
            throw new SongRepositoryException("An error occurred while editing song.", ex);
        }
    }

    public bool DeleteSong(int id)
    {
        using MySqlConnection mySqlConnection = new MySqlConnection(connectionStr);
        MySqlCommand cmd = new MySqlCommand("DELETE FROM songs WHERE id = @id", mySqlConnection);
        cmd.Parameters.AddWithValue("@id", id);

        try
        {
            mySqlConnection.Open();

            return cmd.ExecuteNonQuery() > 0;
        } catch (MySqlException ex)
        {
            throw new SongRepositoryException("An error occurred while deleting song.", ex);
        }
    }
}