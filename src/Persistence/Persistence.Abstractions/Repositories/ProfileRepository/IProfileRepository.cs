using Persistence.Abstractions.Repositories.ProfileRepository.Records;

namespace Persistence.Abstractions.Repositories.ProfileRepository;

public interface IProfileRepository
{
    Task<IReadOnlyList<ProfileRecord>> FindByFirstAndLastName(string firstName, string lastName,
        CancellationToken ct);
}