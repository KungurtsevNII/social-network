using Application.Features.NewsLines.Queries.GetNewsLinesByUserId;
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

    [HttpGet("news-line")]
    public Task<GetNewsLinesByUserIdQueryResult> GetNewsLinesByUserId(CancellationToken ct)
    {
        var userId = _currentUserContext.UserId;
        return _mediator.Send(new GetNewsLinesByUserIdQuery(userId), ct);
    }
}