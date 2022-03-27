using Application.Features.Users.Command.AddFriend;
using Application.Features.Users.Queries.GetFriendsList;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("users")]
public sealed class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserContext _currentUserContext;

    public UsersController(IMediator mediator, ICurrentUserContext currentUserContext)
    {
        _mediator = mediator;
        _currentUserContext = currentUserContext;
    }
    
    [HttpPost("friends")]
    public Task AddFriend(AddFriendCommand command, CancellationToken ct)
    {
        command.CurrentUserId = _currentUserContext.UserId;
        return _mediator.Send(command, ct);
    }
    
    [HttpGet("friends")]
    public Task<GetFriendsListQueryResult> GetFriendsList(CancellationToken ct)
    {
        var query = new GetFriendsListQuery(_currentUserContext.UserId);
        return _mediator.Send(query, ct);
    }
}