using Domain.PostAggregate;

namespace Persistence.Abstractions.Repositories.PostRepository;

public interface IPostRepository
{
    Task SaveAsync(Post post, CancellationToken ct);
    Task<IReadOnlyList<Post>> GetPostsByIds(IReadOnlyList<Guid> ids, CancellationToken ct);
    Task<IReadOnlyList<Post>> GetPostsByUsersIds(IReadOnlyList<long> usersIds, CancellationToken ct);
}