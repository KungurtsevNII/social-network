using MediatR;

namespace Application.Features.NewsLines.Command.UpdateUsersNewsLines;

public record UpdateUsersNewsLinesCommand(
    long UserId,
    Guid PostId) : IRequest;