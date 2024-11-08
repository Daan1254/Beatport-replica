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
    }

    public SongDto CreateSong(CreateEditSongDto createEditSongDto)
    {
        using (MySqlConnection mySqlConnection = new MySqlConnection(connectionStr))
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO songs (title, genre, bpm) VALUES (@title, @genre, @bpm)",
                mySqlConnection);
            cmd.Parameters.AddWithValue("@title", createEditSongDto.Title);
            cmd.Parameters.AddWithValue("@genre", createEditSongDto.Genre);
            cmd.Parameters.AddWithValue("@bpm", createEditSongDto.Bpm);

            mySqlConnection.Open();

            cmd.ExecuteNonQuery();

            mySqlConnection.Close();
        }

        return new SongDto();
    }
}