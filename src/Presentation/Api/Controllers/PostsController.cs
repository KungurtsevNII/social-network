using Application.Features.Posts.Command.CreatePost;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("posts")]
public sealed class PostsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserContext _currentUserContext;

    public PostsController(IMediator mediator, ICurrentUserContext currentUserContext)
    {
        _mediator = mediator;
        _currentUserContext = currentUserContext;
    }
    
    [HttpPost]
    public Task CreatePost(
        string text,
        CancellationToken ct)
    {
        return _mediator.Send(
            new CreatePostCommand(
                _currentUserContext.UserId, 
                text), 
            ct);
    }
}