﻿using Beatport_BLL.Interfaces;
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
        try
        {
            List<SongDto> songs = new List<SongDto>();
            using (MySqlConnection mySqlConnection = new MySqlConnection(connectionStr))
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM songs", mySqlConnection);
            
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
            }
            return songs;
        } catch (Exception e)
        {
            throw new Exception("An error occurred while fetching songs.", e);
        }
    }
    
    public SongDto? GetSong(int id)
    {
        try
        {
            SongDto? song = null;
            using (MySqlConnection mySqlConnection = new MySqlConnection(connectionStr))
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM songs WHERE id = @id", mySqlConnection);
                cmd.Parameters.AddWithValue("@id", id);
            
                mySqlConnection.Open();
            
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
                        UpdatedAt = reader.IsDBNull(reader.GetOrdinal("UpdatedAt")) ? (DateTime?)null : reader.GetDateTime("UpdatedAt"),
                        DeletedAt = reader.IsDBNull(reader.GetOrdinal("DeletedAt")) ? (DateTime?)null : reader.GetDateTime("DeletedAt"),
                    };
                }
            
                mySqlConnection.Close();
            }
            return song;  
        } catch (Exception e)
        {
            throw new Exception("An error occurred while fetching song.", e);
        }
    }

    public bool CreateSong(CreateEditSongDto createEditSongDto)
    {
        try
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(connectionStr))
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO songs (title, genre, bpm) VALUES (@title, @genre, @bpm)",
                    mySqlConnection);
                cmd.Parameters.AddWithValue("@title", createEditSongDto.Title);
                cmd.Parameters.AddWithValue("@genre", createEditSongDto.Genre);
                cmd.Parameters.AddWithValue("@bpm", createEditSongDto.Bpm);

                mySqlConnection.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while creating song.", e);
        }
        
    }
    
    
    public bool EditSong(int id, CreateEditSongDto createEditSongDto)
    {
        try
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(connectionStr))
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE songs SET title = @title, genre = @genre, bpm = @bpm WHERE id = @id",
                    mySqlConnection);
                cmd.Parameters.AddWithValue("@title", createEditSongDto.Title);
                cmd.Parameters.AddWithValue("@genre", createEditSongDto.Genre);
                cmd.Parameters.AddWithValue("@bpm", createEditSongDto.Bpm);
                cmd.Parameters.AddWithValue("@id", id);

                mySqlConnection.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        } catch (Exception e)
        {
            throw new Exception("An error occurred while editing song.", e);
        }
    }

    public bool DeleteSong(int id)
    {
        try
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(connectionStr))
            {
                MySqlCommand cmd = new MySqlCommand("DELETE FROM songs WHERE id = @id", mySqlConnection);
                cmd.Parameters.AddWithValue("@id", id);

                mySqlConnection.Open();

                return cmd.ExecuteNonQuery() > 0;
            }
        } catch (Exception e)
        {
            throw new Exception("An error occurred while deleting song.", e);
        }
    }
}