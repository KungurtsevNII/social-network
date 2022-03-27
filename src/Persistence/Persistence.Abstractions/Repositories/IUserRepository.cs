using Domain.UserAggregate;

namespace Persistence.Abstractions.Repositories;

public interface IUserRepository
{
    Task<User?> FindByEmailAsync(string normalizedEmail, CancellationToken ct);
    Task SaveAsync(User user, CancellationToken ct);
    Task<User?> FindByIdAsync(long id, CancellationToken ct);
}