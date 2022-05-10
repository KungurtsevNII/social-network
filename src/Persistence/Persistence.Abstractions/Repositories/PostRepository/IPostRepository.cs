using Domain.PostAggregate;

namespace Persistence.Abstractions.Repositories.PostRepository;

public interface IPostRepository
{
    Task SaveAsync(Post post, CancellationToken ct);
}