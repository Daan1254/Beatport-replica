namespace Beatport_BLL.Exceptions;

public class PlaylistServiceException : Exception
{
    public PlaylistServiceException(string message) : base(message)
    {
    }

    public PlaylistServiceException(string message, Exception innerException) : base(message, innerException)
    { 
    }
}