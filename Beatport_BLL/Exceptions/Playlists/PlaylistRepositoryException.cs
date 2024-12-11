namespace Beatport_BLL.Exceptions;

public class PlaylistRepositoryException : Exception
{
    public PlaylistRepositoryException(string message) : base(message)
    {
    }

    public PlaylistRepositoryException(string message, Exception innerException) : base(message, innerException)
    {
    }
}