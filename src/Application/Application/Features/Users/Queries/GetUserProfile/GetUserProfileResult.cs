using Application.Dtos;

namespace Application.Features.Users.Queries.GetUserProfile;

public sealed record GetUserProfileResult(ProfileDto Profile);