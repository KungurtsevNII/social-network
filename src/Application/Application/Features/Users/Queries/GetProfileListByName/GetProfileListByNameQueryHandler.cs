using Application.Dtos;
using MediatR;
using Persistence.Abstractions.Repositories.ProfileRepository;

namespace Application.Features.Users.Queries.GetProfileListByName;

public sealed class GetProfileListByNameQueryHandler 
    : IRequestHandler<GetProfileListByNameQuery, GetProfileListByNameQueryResult>
{
    private readonly IProfileRepository _profileRepository;

    public GetProfileListByNameQueryHandler(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public async Task<GetProfileListByNameQueryResult> Handle(
        GetProfileListByNameQuery query,
        CancellationToken ct)
    {
        var profileRecords = await _profileRepository.FindByFirstAndLastName(query.FirstName, query.LastName, ct);

        return new GetProfileListByNameQueryResult(
            profileRecords.Select(x => 
                new ProfileDto(
                    x.FirstName,
                    x.LastName, 
                    x.MiddleName, 
                    x.Age, 
                    x.Sex, 
                    x.Interests, 
                    x.City)).ToList());
    }
}