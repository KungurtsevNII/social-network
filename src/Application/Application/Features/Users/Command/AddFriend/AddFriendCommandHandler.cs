using Application.Exceptions;
using MediatR;
using Persistence.Abstractions.Repositories.UserRepository;

namespace Application.Features.Users.Command.AddFriend;

public sealed class AddFriendCommandHandler : IRequestHandler<AddFriendCommand>
{
    private readonly IUserRepository _userRepository;

    public AddFriendCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(AddFriendCommand command, CancellationToken ct)
    {
        var currentUser = await _userRepository.FindByIdAsync(command.CurrentUserId, ct);
        if (currentUser is null)
        {
            throw new UserNotFoundException(command.CurrentUserId);
        }
        
        var userFriend = await _userRepository.FindByIdAsync(command.NewFriendId, ct);
        if (userFriend is null)
        {
            throw new UserNotFoundException(command.NewFriendId);
        }

        currentUser.AddFriend(userFriend.Id);
        await _userRepository.SaveAsync(currentUser, ct);
        return Unit.Value;
    }
}