namespace Beatport_BLL.Exceptions;

public class SongNotFoundException : Exception
{
    public int SongId { get; } // Optional custom property to store the ID of the missing song.

    // Default constructor
    public SongNotFoundException() : base("The specified song was not found.")
    {
    }

    // Constructor that accepts a custom message
    public SongNotFoundException(string message) : base(message)
    {
    }

    // Constructor that accepts a custom message and an inner exception (for wrapping other exceptions)
    public SongNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    // Constructor that accepts a custom message and the song ID
    public SongNotFoundException(int songId) 
        : base($"Song with ID {songId} was not found.")
    {
        SongId = songId;
    }
}