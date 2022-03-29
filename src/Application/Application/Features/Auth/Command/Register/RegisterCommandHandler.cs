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

        var userId = await _userRepository.SaveAsync(user, ct);

        var profile = Profile.Create(
            userId,
            command.Profile.FirstName,
            command.Profile.LastName,
            command.Profile.MiddleName,
            command.Profile.Age,
            command.Profile.Sex,
            command.Profile.Interests,
            command.Profile.City);
        
        await _userRepository.SaveProfileAsync(profile, ct);
        return Unit.Value;
    }
}