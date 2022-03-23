using MediatR;

namespace Application.Features.Auth.Command.Register;

public sealed record RegisterCommand(
    string Email,
    string Password,
    string? PhoneNumber,
    bool TwoFactorEnabled) : IRequest;