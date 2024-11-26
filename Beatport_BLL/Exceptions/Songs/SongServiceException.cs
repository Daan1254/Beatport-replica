namespace Beatport_BLL.Exceptions;

public class SongServiceException : Exception
{
    public SongServiceException(string message) : base(message)
    {
    }

    public SongServiceException(string message, Exception innerException) : base(message, innerException)
    {
    }
}