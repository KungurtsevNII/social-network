using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Persistence.Abstractions;

namespace Persistence.Postgres;

public sealed class DbContext : IDbContext
{
    private readonly IReadOnlyDictionary<string, string> _connectionString;
    
    public DbContext(IConfiguration configuration)
    {
        _connectionString = configuration
            .GetSection("ConnectionStrings")
            .GetChildren()
            .ToDictionary(x => x.Key, y => y.Value);
    }
    
    public IDbConnection CreateMasterConnection() => new NpgsqlConnection(_connectionString["Main"]);
    public IDbConnection CreateReplicationConnection() => new NpgsqlConnection(_connectionString["Main"]);
}