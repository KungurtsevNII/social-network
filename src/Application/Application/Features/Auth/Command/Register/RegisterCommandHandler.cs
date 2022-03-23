using System.Security.Cryptography;
using System.Text;
using Application.Exceptions.Base;
using Domain.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Command.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly IUserStore<User> _userStore;

    public RegisterCommandHandler(IUserStore<User> userStore)
    {
        _userStore = userStore;
    }

    public async Task<Unit> Handle(RegisterCommand command, CancellationToken ct)
    {
        var existingUser = await _userStore.FindByNameAsync(command.Email, ct);
        if (existingUser is not null)
        {
            throw new UserAlreadyExistsException(command.Email);
        }
        var user = User.Create(
            command.Email, 
            Hash(command.Password), 
            command.PhoneNumber, 
            command.TwoFactorEnabled, 
            new List<Role>());

        var identityResult = await _userStore.CreateAsync(user, ct);
        return Unit.Value;
    }

    private string Hash(string password)
    {
        using var md5 = MD5.Create();
        var result = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Encoding.UTF8.GetString(result);
    }
}