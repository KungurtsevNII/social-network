using Application.Exceptions.Base;
using Application.Services;
using Domain.UserAggregate;
using MediatR;
using Persistence.Abstractions.Repositories;

namespace Application.Features.Auth.Command.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherService _passwordHasherService;

    public RegisterCommandHandler(
        IUserRepository userStore,
        IPasswordHasherService passwordHasherService)
    {
        _userRepository = userStore;
        _passwordHasherService = passwordHasherService;
    }

    public async Task<Unit> Handle(RegisterCommand command, CancellationToken ct)
    {
        var existingUser = await _userRepository.FindByEmailAsync(command.Email, ct);
        if (existingUser is not null)
        {
            throw new UserAlreadyExistsException(command.Email);
        }
        
        var user = User.Create(
            command.Email, 
            _passwordHasherService.ComputeHash(command.Password), 
            command.PhoneNumber, 
            command.TwoFactorEnabled, 
            new List<Role>());

        await _userRepository.SaveAsync(user, ct);
        return Unit.Value;
    }
}