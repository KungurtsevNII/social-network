using Application.Services;
using MediatR;

namespace Application.Features.Users.Command.AddFriend;

public sealed class AddFriendCommandHandler : IRequestHandler<AddFriendCommand>
{
    private readonly ICurrentUserContext _currentUserContext;

    public AddFriendCommandHandler(ICurrentUserContext currentUserContext)
    {
        _currentUserContext = currentUserContext;
    }

    public Task<Unit> Handle(AddFriendCommand request, CancellationToken cancellationToken)
    {
        var s = _currentUserContext.UserId;
        return Task.FromResult(Unit.Value);
    }
}