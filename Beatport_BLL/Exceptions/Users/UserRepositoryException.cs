namespace Beatport_BLL.Exceptions;

public class UserRepositoryException : Exception
{
    public UserRepositoryException(string message) : base(message)
    {
    }
    
    public UserRepositoryException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
} 