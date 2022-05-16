using System.Data;
using Dapper;
using Domain.NewsLineAggregate;
using Persistence.Abstractions;
using Persistence.Abstractions.Repositories.NewsLineRepository;
using Persistence.Abstractions.Repositories.NewsLineRepository.Records;

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

    public async Task<IReadOnlyList<NewsLine>> GetNewsLinesBynNewsLineOwnerUserId(long newsLineOwnerUserId, CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("newsLineOwnerUserId", newsLineOwnerUserId, DbType.Int64);
        
        using var pgConnection = _dbContext.CreateReplicationConnection();
        pgConnection.Open();
        var newsLinesRecords = await pgConnection.QueryAsync<NewsLineRecord>(
            NewsLineRepositorySql.GetNewsLinesBynNewsLineOwnerUserIdSql, 
            parameters);
        
        var newsLines = newsLinesRecords.Select(x => new NewsLine(
            x.Id,
            x.PostId,
            x.PostCreaterUserId,
            x.NewsLineOwnerUserId)).ToList();
        
        return newsLines;
    }
}