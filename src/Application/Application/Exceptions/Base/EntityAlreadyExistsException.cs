namespace Application.Exceptions.Base;

public abstract class EntityAlreadyExistsException : Exception
{
    protected EntityAlreadyExistsException(string message) : base(message)
    {
    }
}