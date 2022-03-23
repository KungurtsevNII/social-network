using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Persistence.Abstractions;

namespace Persistence.Postgres;

public sealed class DbContext : IDbContext
{
    private readonly string _connectionString;
    
    public DbContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Main");
    }
    
    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}