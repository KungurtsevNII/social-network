using Application.Services;
using MediatR;
using Persistence.Abstractions.Repositories;

namespace Application.Features.Users.Command.AddFriend;

public sealed class AddFriendCommandHandler : IRequestHandler<AddFriendCommand>
{
    private readonly ICurrentUserContext _currentUserContext;
    private readonly IUserRepository _userRepository;

    public AddFriendCommandHandler(
        ICurrentUserContext currentUserContext,
        IUserRepository userRepository)
    {
        _currentUserContext = currentUserContext;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(AddFriendCommand command, CancellationToken ct)
    {
        var currentUserId = _currentUserContext.UserId;
        var currentUser = await _userRepository.FindByIdAsync(currentUserId, ct);
        return Unit.Value;
    }
}