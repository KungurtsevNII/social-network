using Dapper;
using Domain.PostAggregate;
using Persistence.Abstractions;
using Persistence.Abstractions.Repositories.PostRepository;

namespace Persistence.Postgres.Repositories.PostRepository;

public sealed class PostRepository : IPostRepository
{
    private readonly IDbContext _dbContext;

    public PostRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task SaveAsync(Post post, CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@id", post.Id);
        parameters.Add("@user_id", post.UserId);
        parameters.Add("@text", post.Text);
        
        using var pgConnection = _dbContext.CreateMasterConnection();
        pgConnection.Open();
        await pgConnection.QuerySingleAsync<long>(PostRepositorySql.SaveSql, parameters);
    }
}