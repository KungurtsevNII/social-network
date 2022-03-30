using Domain.UserAggregate;

namespace Persistence.Abstractions.Repositories;

public interface IUserRepository
{
    Task<User?> FindByEmailAsync(string normalizedEmail, CancellationToken ct);
    Task<long> SaveAsync(User user, CancellationToken ct);
    Task<User?> FindByIdAsync(long id, CancellationToken ct);
    Task SaveProfileAsync(Profile profile, CancellationToken ct);
    Task<bool> IsExistsAsync(string normalizedEmail, CancellationToken ct);
}