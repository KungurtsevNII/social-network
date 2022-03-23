using MediatR;

namespace Application.Features.Auth.Command.Login;

public sealed record LoginCommand(
    string Email,
    string Password) : IRequest<LoginResult>;