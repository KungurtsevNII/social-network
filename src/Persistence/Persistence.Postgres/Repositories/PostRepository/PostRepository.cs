using Dapper;
using Domain.PostAggregate;
using Persistence.Abstractions;
using Persistence.Abstractions.Repositories.PostRepository;
using Persistence.Abstractions.Repositories.PostRepository.Records;

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
        parameters.Add("@userId", post.UserId);
        parameters.Add("@text", post.Text);
        parameters.Add("@createdAt", post.CreatedAt);
        
        using var pgConnection = _dbContext.CreateMasterConnection();
        pgConnection.Open();
        await pgConnection.ExecuteAsync(PostRepositorySql.SaveSql, parameters);
    }

    public async Task<IReadOnlyList<Post>> GetPostsByIds(IReadOnlyList<Guid> ids, CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ids", ids);

        using var pgConnection = _dbContext.CreateMasterConnection();
        pgConnection.Open();
        var postsRecords = await pgConnection.QueryAsync<PostRecord>(PostRepositorySql.GetPostsByIdsSql, parameters);

        return postsRecords
            .Select(x => 
                new Post(
                    x.Id,
                    x.UserId, 
                    x.Text, 
                    x.CreatedAt))
            .ToList();
    }

    public async Task<Post> GetPostById(Guid id, CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@id", id);

        using var pgConnection = _dbContext.CreateReplicationConnection();
        pgConnection.Open();
        var postsRecord = await pgConnection.QuerySingleOrDefaultAsync<PostRecord>(PostRepositorySql.GetPostByIdSql, parameters);

        return new Post(
            postsRecord.Id, 
            postsRecord.UserId, 
            postsRecord.Text,
            postsRecord.CreatedAt);
    }

    public async Task<IReadOnlyList<Post>> GetPostsByUsersIds(IReadOnlyList<long> usersIds, CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@usersIds", usersIds);

        using var pgConnection = _dbContext.CreateMasterConnection();
        pgConnection.Open();
        var postsRecords = await pgConnection.QueryAsync<PostRecord>(PostRepositorySql.GetCelebrityPostsSql, parameters);

        return postsRecords
            .Select(x => 
                new Post(
                    x.Id,
                    x.UserId, 
                    x.Text, 
                    x.CreatedAt))
            .ToList();
    }
}