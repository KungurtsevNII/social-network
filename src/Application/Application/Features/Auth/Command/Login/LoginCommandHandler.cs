using Application.Exceptions;
using Application.Services;
using MediatR;
using Persistence.Abstractions.Repositories;

namespace Application.Features.Auth.Command.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IPasswordHasherService _passwordHasherService;

    public LoginCommandHandler(
        IJwtTokenGenerator tokenGenerator, 
        IPasswordHasherService passwordHasherService,
        IUserRepository userRepository)
    {
        _tokenGenerator = tokenGenerator;
        _passwordHasherService = passwordHasherService;
        _userRepository = userRepository;
    }

    public async Task<LoginResult> Handle(LoginCommand command, CancellationToken ct)
    {
        var user = await _userRepository.FindByEmailAsync(command.Email, ct);
        if (user is null)
        {
            throw new UserNotFoundException(command.Email);
        }

        var isPasswordVerified = user.PasswordHash == _passwordHasherService.ComputeHash(command.Password);

        if (!isPasswordVerified)
        {
            throw new LoginException();
        }

        return new LoginResult(_tokenGenerator.CreateToken(user));
    }
}