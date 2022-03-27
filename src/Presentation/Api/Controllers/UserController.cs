using Application.Features.Users.Command.AddFriend;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("friends")]
public sealed class FriendsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FriendsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public Task AddFriend(AddFriendCommand command, CancellationToken ct)
    {
        return _mediator.Send(command, ct);
    }
}