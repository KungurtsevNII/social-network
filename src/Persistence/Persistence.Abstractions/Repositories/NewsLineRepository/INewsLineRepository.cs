using Domain.NewsLineAggregate;

namespace Persistence.Abstractions.Repositories.NewsLineRepository;

public interface INewsLineRepository
{
    Task SaveAsync(IReadOnlyList<NewsLine> newsLines, CancellationToken ct);
    Task<IReadOnlyList<NewsLine>> GetNewsLinesBynNewsLineOwnerUserId(long newsLineOwnerUserId, CancellationToken ct);
}