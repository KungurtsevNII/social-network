using MediatR;

namespace Application.Features.NewsLines.Command.AddPostInNewLineOnline;

public sealed record AddPostInNewLineOnlineCommand(
    long UserId,
    Guid PostId) : IRequest;

