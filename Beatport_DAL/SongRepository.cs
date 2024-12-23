﻿using Beatport_BLL.Exceptions;
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
    
    public List<SongDto> GetAllSongs(int? userId)
    {
        List<SongDto> songs = new List<SongDto>();
        using MySqlConnection mySqlConnection = new MySqlConnection(connectionStr);
        
        string query = userId.HasValue 
            ? "SELECT * FROM songs WHERE user_id = @userId AND DeletedAt IS NULL"
            : "SELECT * FROM songs WHERE DeletedAt IS NULL";
        
        using MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
        
        if (userId.HasValue)
        {
            cmd.Parameters.AddWithValue("@userId", userId);
        }
        
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
                    FilePath = reader.GetString("file_path"),
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

    public SongDto? GetSong(int id, int? userId)
    {
        SongDto? song = null;
        using MySqlConnection mySqlConnection = new MySqlConnection(connectionStr);
        
        string query = userId.HasValue
            ? "SELECT * FROM songs WHERE id = @id AND user_id = @userId AND DeletedAt IS NULL"
            : "SELECT * FROM songs WHERE id = @id AND DeletedAt IS NULL";
        
        using MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
        
        cmd.Parameters.AddWithValue("@id", id);
        if (userId.HasValue)
        {
            cmd.Parameters.AddWithValue("@userId", userId);
        }

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
        using MySqlCommand cmd = new MySqlCommand(
            "INSERT INTO songs (title, genre, bpm, file_path, user_id) VALUES (@title, @genre, @bpm, @filePath, @userId)", 
            mySqlConnection);
        
        cmd.Parameters.AddWithValue("@title", createEditSongDto.Title);
        cmd.Parameters.AddWithValue("@genre", createEditSongDto.Genre);
        cmd.Parameters.AddWithValue("@bpm", createEditSongDto.Bpm);
        cmd.Parameters.AddWithValue("@filePath", createEditSongDto.FilePath);
        cmd.Parameters.AddWithValue("@userId", createEditSongDto.UserId);
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

    public async Task<int> GetTotalSongsByUser(int userId)
    {
        using MySqlConnection connection = new MySqlConnection(connectionStr);
        using MySqlCommand cmd = new MySqlCommand(
            "SELECT COUNT(*) FROM songs WHERE user_id = @userId", connection);
        
        cmd.Parameters.AddWithValue("@userId", userId);
        
        try
        {
            await connection.OpenAsync();
            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }
        catch (MySqlException ex)
        {
            throw new SongRepositoryException("Error getting total songs count", ex);
        }
    }
}