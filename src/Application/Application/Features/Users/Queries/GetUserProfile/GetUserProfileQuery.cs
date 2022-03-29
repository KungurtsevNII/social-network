using MediatR;

namespace Application.Features.Users.Queries.GetUserProfile;

public sealed record GetUserProfileQuery(long UserId) : IRequest<GetUserProfileResult>;