using Application.Features.Users.Queries.GetProfileListByName;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("profiles")]
public sealed class ProfilesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProfilesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("by-name")]
    public Task<GetProfileListByNameQueryResult> GetProfileListByName(
        [FromQuery] GetProfileListByNameQuery query,
        CancellationToken ct)
    {
        return _mediator.Send(query, ct);
    }
}