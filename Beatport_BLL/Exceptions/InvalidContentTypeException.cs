namespace Beatport_BLL.Exceptions;

public class InvalidContentTypeException : Exception
{
    public InvalidContentTypeException() : base("Invalid content type.")
    {
        
    }
    
    public InvalidContentTypeException(string message) : base(message)
    {
        
    }
}