namespace Beatport_BLL.Exceptions;

public class SongRepositoryException : Exception
{
    public SongRepositoryException(string message) : base(message)
    {
    }

    public SongRepositoryException(string message, Exception innerException) : base(message, innerException)
    {
    }
}