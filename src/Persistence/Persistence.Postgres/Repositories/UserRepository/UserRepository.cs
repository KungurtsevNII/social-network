using System.Data;
using Dapper;
using Domain.UserAggregate;
using Persistence.Abstractions;
using Persistence.Abstractions.Repositories.UserRepository;
using Persistence.Abstractions.Repositories.UserRepository.Records;

namespace Persistence.Postgres.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly IDbContext _dbContext;

    public UserRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<long> SaveAsync(User user, CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@email", user.Email);
        parameters.Add("@normalizedEmail", user.NormalizedEmail);
        parameters.Add("@emailConfirmed", user.EmailConfirmed);
        parameters.Add("@passwordHash", user.PasswordHash);
        parameters.Add("@phoneNumber", user.PhoneNumber);
        parameters.Add("@phoneNumberConfirmed", user.PhoneNumberConfirmed);
        parameters.Add("@twoFactorEnabled", user.TwoFactorEnabled);
        
        using var pgConnection = _dbContext.CreateMasterConnection();
        pgConnection.Open();
        var userId = await pgConnection.QuerySingleAsync<long>(UserRepositorySql.SaveSql, parameters);
        await pgConnection.ExecuteAsync(
            UserRepositorySql.SaveFriendsSql, 
            user.Friends.Select(x => new 
            { 
                userId = user.Id, 
                friendId = x 
            }));

        return userId;
    }
    
    public async Task SaveProfileAsync(Profile profile, CancellationToken ct)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@userId", profile.Id);
        parameters.Add("@firstName", profile.FirstName);
        parameters.Add("@lastName", profile.LastName);
        parameters.Add("@middleName", profile.MiddleName);
        parameters.Add("@age", profile.Age);
        parameters.Add("@sex", profile.Sex);
        parameters.Add("@interests", profile.Interests);
        parameters.Add("@city", profile.City);
        
        using var pgConnection = _dbContext.CreateMasterConnection();
        pgConnection.Open();
        await pgConnection.ExecuteAsync(UserRepositorySql.SaveProfileSql, parameters);
    }
    
    public async Task<User?> FindByEmailAsync(string normalizedEmail, CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("normalizedEmail", normalizedEmail.ToLower(), DbType.String);
        
        using var pgConnection = _dbContext.CreateReplicationConnection();
        pgConnection.Open();
        var userRecord = await pgConnection.QuerySingleOrDefaultAsync<UserRecord>(UserRepositorySql.FindByNormalizedSql, parameters);

        if (userRecord is null)
        {
            return null;
        }
        
        var userFriend = await GetUserFriends(pgConnection, userRecord.Id, ct);
        var userProfile = await GetUserProfile(pgConnection, userRecord.Id, ct);
        return new User(
            userRecord.Id,
            userRecord.Email,
            userRecord.NormalizedEmail,
            userRecord.EmailConfirmed,
            userRecord.PasswordHash,
            userRecord.PhoneNumber,
            userRecord.PhoneNumberConfirmed,
            userRecord.TwoFactorEnabled,
            new List<Role>(),
            userFriend.ToList(),
            userProfile);
    }
    
    public async Task<bool> IsExistsAsync(string normalizedEmail, CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("normalizedEmail", normalizedEmail.ToLower(), DbType.String);
        
        using var pgConnection = _dbContext.CreateReplicationConnection();
        pgConnection.Open();
        var userRecord = await pgConnection.QuerySingleOrDefaultAsync<UserRecord>(UserRepositorySql.FindByNormalizedSql, parameters);

        return userRecord is not null;
    }

    public async Task<User?> FindByIdAsync(long id, CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("id", id, DbType.Int64);
        
        using var pgConnection = _dbContext.CreateReplicationConnection();
        pgConnection.Open();
        var userRecord = await pgConnection.QuerySingleOrDefaultAsync<UserRecord>(UserRepositorySql.FindByIdSql, parameters);

        if (userRecord is null)
        {
            return null;
        }

        var userFriend = await GetUserFriends(pgConnection, userRecord.Id, ct);
        var userProfile = await GetUserProfile(pgConnection, userRecord.Id, ct);
        
        return new User(
            userRecord.Id,
            userRecord.Email,
            userRecord.NormalizedEmail,
            userRecord.EmailConfirmed,
            userRecord.PasswordHash,
            userRecord.PhoneNumber,
            userRecord.PhoneNumberConfirmed,
            userRecord.TwoFactorEnabled,
            new List<Role>(),
            userFriend.ToList(),
            userProfile);
    }
    
    public async Task<long> GetFriendsCountAsync(long userId, CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("userId", userId, DbType.Int64);
        
        using var pgConnection = _dbContext.CreateReplicationConnection();
        pgConnection.Open();
        var friendsCount = await pgConnection.QuerySingleAsync<long>(UserRepositorySql.GetFriendsCountSql, parameters);

        return friendsCount;
    }
    
    public async Task<IReadOnlyList<long>> GetUserFriends(long userId, CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("userId", userId, DbType.Int64);
        
        using var pgConnection = _dbContext.CreateReplicationConnection();
        pgConnection.Open();
        
        var friendsRecords = await pgConnection.QueryAsync<FriendRecord>(UserRepositorySql.FindFriendsIdsSql, parameters);
        var friendList = friendsRecords.ToList();
        
        var friends= friendList
            .Select(x => x.FriendId)
            .Union(friendList.Select(x => x.UserId));
        
        return friends.ToList();
    }

    private async Task<Profile> GetUserProfile(IDbConnection pgConnection, long userRecordId, CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("userId", userRecordId, DbType.Int64);
        var profileRecord = await pgConnection.QuerySingleOrDefaultAsync<ProfileRecord>(UserRepositorySql.FindProfileByIdSql, parameters);
        
        return new Profile(
            profileRecord.UserId,
            profileRecord.FirstName,
            profileRecord.LastName,
            profileRecord.MiddleName,
            profileRecord.Age,
            profileRecord.Sex,
            profileRecord.Interests,
            profileRecord.City);
    }

    private async Task<IReadOnlyList<long>> GetUserFriends(IDbConnection pgConnection, long userId, CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("userId", userId, DbType.Int64);
        
        var friendsIds = await pgConnection.QueryAsync<long>(UserRepositorySql.FindFriendsIdsSql, parameters);
        return friendsIds.ToList();
    }
}