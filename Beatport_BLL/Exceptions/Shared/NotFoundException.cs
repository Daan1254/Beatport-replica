namespace Beatport_BLL.Exceptions;

public class NotFoundException : Exception
{
    // Constructor that accepts a custom message
    public NotFoundException(string message) : base(message)
    {
    }

    // Constructor that accepts a custom message and an inner exception (for wrapping other exceptions)
    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
    
}