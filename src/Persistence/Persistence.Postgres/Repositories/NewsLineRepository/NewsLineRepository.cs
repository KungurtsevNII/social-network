using Dapper;
using Domain.NewsLineAggregate;
using Persistence.Abstractions;
using Persistence.Abstractions.Repositories.NewsLineRepository;

namespace Persistence.Postgres.Repositories.NewsLineRepository;

public sealed class NewsLineRepository : INewsLineRepository
{
    private readonly IDbContext _dbContext;

    public NewsLineRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveAsync(IReadOnlyList<NewsLine> newsLines, CancellationToken ct)
    {
        using var pgConnection = _dbContext.CreateMasterConnection();
        pgConnection.Open();
        await pgConnection.ExecuteAsync(
            NewsLineRepositorySql.SaveSql,
            newsLines.Select(x => new
            {
                id = x.Id,
                postId = x.PostId,
                postCreaterUserId = x.PostCreaterUserId,
                newsLineOwnerUserId = x.NewsLineOwnerUserId
            }));
    }
}