using Application.Dtos;

namespace Application.Features.Users.Queries.GetProfileListByName;

public sealed record GetProfileListByNameQueryResult(IReadOnlyList<ProfileDto> Profiles);