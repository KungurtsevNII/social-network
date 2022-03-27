using MediatR;

namespace Application.Features.Users.Command.AddFriend;

public sealed record AddFriendCommand(long NewFriend) : IRequest;