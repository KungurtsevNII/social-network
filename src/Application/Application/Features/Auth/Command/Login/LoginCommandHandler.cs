using System.Security.Cryptography;
using System.Text;
using Application.Exceptions;
using Application.Services;
using Domain.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Command.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly IUserStore<User> _userStore;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public LoginCommandHandler(IUserStore<User> userStore, IJwtTokenGenerator tokenGenerator)
    {
        _userStore = userStore;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<LoginResult> Handle(LoginCommand command, CancellationToken ct)
    {
        var user = await _userStore.FindByNameAsync(command.Email, ct);
        if (user is null)
        {
            throw new UserNotFoundException(command.Email);
        }

        var isPasswordVerified = user.PasswordHash == Hash(command.Password);

        if (!isPasswordVerified)
        {
            throw new LoginException();
        }

        return new LoginResult(_tokenGenerator.CreateToken(user));
    }
    
    private string Hash(string password)
    {
        using var md5 = MD5.Create();
        var result = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Encoding.UTF8.GetString(result);
    }
}