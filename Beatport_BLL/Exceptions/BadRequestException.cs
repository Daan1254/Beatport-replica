namespace Beatport_BLL.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException() : base("An unknown error occurred.")
    {
    }
    
    public BadRequestException(string message) : base(message)
    {
    }
    
    public BadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
