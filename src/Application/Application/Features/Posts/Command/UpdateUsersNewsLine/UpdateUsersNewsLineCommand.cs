using MediatR;

namespace Application.Features.Posts.Command.UpdateUsersNewsLine;

public record UpdateUsersNewsLineCommand(
    long UserId,
    Guid PostId) : IRequest;