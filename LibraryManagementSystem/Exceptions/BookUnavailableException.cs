namespace LibraryManagementSystem;

public class BookUnavailableException : Exception
{
    public BookUnavailableException(string message) : base(message)
    {
    }

    public BookUnavailableException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
