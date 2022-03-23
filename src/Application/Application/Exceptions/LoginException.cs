using Application.Exceptions.Base;

namespace Application.Exceptions;

public sealed class LoginException : NotFoundException
{
    public LoginException() : base("Could not find a matching login password pair")
    {
        
    }
}