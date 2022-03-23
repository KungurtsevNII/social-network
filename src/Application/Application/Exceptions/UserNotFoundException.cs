using Application.Exceptions.Base;

namespace Application.Exceptions;

public sealed class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string email) 
        : base($"User with email - {email} not found")
    {
    }
}