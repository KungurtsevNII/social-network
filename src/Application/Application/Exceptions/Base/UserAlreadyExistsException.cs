namespace Application.Exceptions.Base;

public sealed class UserAlreadyExistsException : EntityAlreadyExistsException
{
    public UserAlreadyExistsException(string email) 
        : base($"User with email - {email} already exists")
    {
    }
}