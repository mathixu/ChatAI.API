namespace ChatAI.Application.Common.Exceptions;

public class AlreadyExistException : Exception
{
    public AlreadyExistException()
        : base()
    {
    }

    public AlreadyExistException(string message)
        : base(message)
    {
    }

    public AlreadyExistException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public AlreadyExistException(string name, object key)
        : base($"Entity \"{name}\" ({key}) already exist.")
    {
    }
}
