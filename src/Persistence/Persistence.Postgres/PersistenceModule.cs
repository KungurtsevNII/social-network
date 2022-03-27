using Microsoft.Extensions.DependencyInjection;
using Persistence.Abstractions;
using Persistence.Abstractions.Repositories;
using Persistence.Postgres.Repositories.UserRepository;

namespace Persistence.Postgres;

public static class PersistenceModule
{
    public static IServiceCollection AddPersistenceModule(this IServiceCollection services)
    {
        services.AddSingleton<IDbContext, DbContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        return services;
    }
}