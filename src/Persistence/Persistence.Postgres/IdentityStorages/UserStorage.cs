using System.Data;
using Dapper;
using Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Persistence.Abstractions;

namespace Persistence.Postgres.IdentityStorages;

public sealed class UserStorage : IUserStore<User>
{
    private readonly IDbContext _dbContext;

    public UserStorage(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
    {
        var query = @"INSERT INTO users (
                   email,
                   normalized_email,
                   email_confirmed,
                   password_hash,
                   phone_number,
                   phone_number_confirmed,
                   two_factor_enabled)
                   VALUES(@email, @normalizedEmail, @emailConfirmed, @passwordHash, @phoneNumber, @phoneNumberConfirmed, @twoFactorEnabled)";
        
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
        await pgConnection.ExecuteAsync(query, parameters);
        return IdentityResult.Success;
    }

    public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> FindByNameAsync(string normalizedUserName, CancellationToken ct)
    {
        var query = @"SELECT id, email, normalized_email, email_confirmed,
                             password_hash, phone_number, phone_number_confirmed, two_factor_enabled
                      FROM users
                      WHERE normalized_email = @normalizedEmail";
        
        var parameters = new DynamicParameters();
        parameters.Add("normalizedEmail", normalizedUserName.ToLower(), DbType.String);
        
        using var pgConnection = _dbContext.CreateConnection();
        pgConnection.Open();
        var userRecord = await pgConnection.QuerySingleOrDefaultAsync<UserRecord>(query, parameters);

        if (userRecord is null)
        {
            return null;
        }
        
        return new User(userRecord.Id, userRecord.Email, userRecord.NormalizedEmail, userRecord.EmailConfirmed,
            userRecord.PasswordHash, userRecord.PhoneNumber, userRecord.PhoneNumberConfirmed,
            userRecord.TwoFactorEnabled, new List<Role>());
    }
    
    public void Dispose()
    {
    }
}