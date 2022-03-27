namespace Application.Features.Users.Queries.GetFriendsList;

public sealed record GetFriendsListQueryResult(IReadOnlyList<long> FriendsIds);