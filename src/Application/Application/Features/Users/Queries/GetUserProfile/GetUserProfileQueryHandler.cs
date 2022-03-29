using Application.Dtos;
using Application.Exceptions;
using MediatR;
using Persistence.Abstractions.Repositories;

namespace Application.Features.Users.Queries.GetUserProfile;

public sealed class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, GetUserProfileResult>
{
    private readonly IUserRepository _userRepository;

    public GetUserProfileQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserProfileResult> Handle(GetUserProfileQuery query, CancellationToken ct)
    {
        var user = await _userRepository.FindByIdAsync(query.UserId, ct);

        if (user is null)
        {
            throw new UserNotFoundException(query.UserId);
        }

        return new GetUserProfileResult(
            new ProfileDto(
                user.Profile.FirstName, 
                user.Profile.LastName, 
                user.Profile.MiddleName, 
                user.Profile.Age, 
                user.Profile.Sex, 
                user.Profile.Interests, 
                user.Profile.City));
    }
}