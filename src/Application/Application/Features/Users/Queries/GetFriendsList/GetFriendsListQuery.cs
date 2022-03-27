using MediatR;

namespace Application.Features.Users.Queries.GetFriendsList;

public sealed record GetFriendsListQuery(long CurrentUserId) : IRequest<GetFriendsListQueryResult>;