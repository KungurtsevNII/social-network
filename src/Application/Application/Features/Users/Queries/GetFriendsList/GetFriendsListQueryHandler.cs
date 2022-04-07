using Application.Exceptions;
using MediatR;
using Persistence.Abstractions.Repositories.UserRepository;

namespace Application.Features.Users.Queries.GetFriendsList;

public sealed class GetFriendsListQueryHandler : IRequestHandler<GetFriendsListQuery, GetFriendsListQueryResult>
{
    private readonly IUserRepository _userRepository;

    public GetFriendsListQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetFriendsListQueryResult> Handle(GetFriendsListQuery query, CancellationToken ct)
    {
        var user = await _userRepository.FindByIdAsync(query.CurrentUserId, ct);
        if (user is null)
        {
            throw new UserNotFoundException(query.CurrentUserId);
        }

        return new GetFriendsListQueryResult(user.Friends);
    }
}