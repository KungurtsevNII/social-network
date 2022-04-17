using System.Data;
using Dapper;
using Persistence.Abstractions;
using Persistence.Abstractions.Repositories.ProfileRepository;
using Persistence.Abstractions.Repositories.ProfileRepository.Records;

namespace Persistence.Postgres.Repositories.ProfileRepository;

public sealed class ProfileRepository : IProfileRepository
{
    private readonly IDbContext _dbContext;

    public ProfileRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IReadOnlyList<ProfileRecord>> FindByFirstAndLastName(string firstName, string lastName, CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("firstName", firstName.ToLower() + "%", DbType.String);
        parameters.Add("lastName", lastName.ToLower() + "%", DbType.String);
        
        using var pgConnection = _dbContext.CreateReplicationConnection();
        pgConnection.Open();
        var profileRecords = await pgConnection.QueryAsync<ProfileRecord>(
            ProfileRepositorySql.FindByFirstAndLastNormalizedName, 
            parameters);

        return profileRecords.ToList();
    }
}