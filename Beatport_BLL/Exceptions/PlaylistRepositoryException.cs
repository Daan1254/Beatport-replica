using System;

public class PlaylistRepositoryException : Exception
{
    public PlaylistRepositoryException() : base() { }
    
    public PlaylistRepositoryException(string message) : base(message) { }
    
    public PlaylistRepositoryException(string message, Exception innerException) 
        : base(message, innerException) { }
} 