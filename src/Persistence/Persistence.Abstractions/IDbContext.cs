using System.Data;

namespace Persistence.Abstractions;

public interface IDbContext
{
    IDbConnection CreateMasterConnection();
    IDbConnection CreateReplicationConnection();
}