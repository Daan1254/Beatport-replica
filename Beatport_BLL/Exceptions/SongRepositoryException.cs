using System;

public class SongRepositoryException : Exception
{
    public SongRepositoryException() : base() { }
    
    public SongRepositoryException(string message) : base(message) { }
    
    public SongRepositoryException(string message, Exception innerException) 
        : base(message, innerException) { }
} 