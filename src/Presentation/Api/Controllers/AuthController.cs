using Application.Features.Auth.Command.Login;
using Application.Features.Auth.Command.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("auth")]
public sealed class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public Task<LoginResult> Login(LoginCommand command, CancellationToken ct)
    {
        return _mediator.Send(command, ct);
    }
    
    [HttpPost("register")]
    public Task Login(RegisterCommand command, CancellationToken ct)
    {
        return _mediator.Send(command, ct);
    }
}