using System.Data;
using Dapper;
using Domain.UserAggregate;
using Persistence.Abstractions;
using Persistence.Abstractions.Repositories;
using Persistence.Postgres.Repositories.UserRepository.Records;

namespace Persistence.Postgres.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly IDbContext _dbContext;

    public UserRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task SaveAsync(User user, CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@email", user.Email);
        parameters.Add("@normalizedEmail", user.NormalizedEmail);
        parameters.Add("@emailConfirmed", user.EmailConfirmed);
        parameters.Add("@passwordHash", user.PasswordHash);
        parameters.Add("@phoneNumber", user.PhoneNumber);
        parameters.Add("@phoneNumberConfirmed", user.PhoneNumberConfirmed);
        parameters.Add("@twoFactorEnabled", user.TwoFactorEnabled);
        
        using var pgConnection = _dbContext.CreateConnection();
        pgConnection.Open();
        await pgConnection.ExecuteAsync(UserRepositorySql.SaveSql, parameters);
    }
    
    public async Task<User?> FindByEmailAsync(string normalizedEmail, CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("normalizedEmail", normalizedEmail.ToLower(), DbType.String);
        
        using var pgConnection = _dbContext.CreateConnection();
        pgConnection.Open();
        var userRecord = await pgConnection.QuerySingleOrDefaultAsync<UserRecord>(UserRepositorySql.FindByNormalizedSql, parameters);

        if (userRecord is null)
        {
            return null;
        }
        
        return new User(
            userRecord.Id,
            userRecord.Email,
            userRecord.NormalizedEmail,
            userRecord.EmailConfirmed,
            userRecord.PasswordHash,
            userRecord.PhoneNumber,
            userRecord.PhoneNumberConfirmed,
            userRecord.TwoFactorEnabled,
            new List<Role>());
    }
}