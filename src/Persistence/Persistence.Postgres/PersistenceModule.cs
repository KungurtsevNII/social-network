using Microsoft.Extensions.DependencyInjection;
using Persistence.Abstractions;
using Persistence.Abstractions.Repositories.NewsLineRepository;
using Persistence.Abstractions.Repositories.PostRepository;
using Persistence.Abstractions.Repositories.ProfileRepository;
using Persistence.Abstractions.Repositories.UserRepository;
using Persistence.Postgres.Repositories.NewsLineRepository;
using Persistence.Postgres.Repositories.PostRepository;
using Persistence.Postgres.Repositories.ProfileRepository;
using Persistence.Postgres.Repositories.UserRepository;

namespace Persistence.Postgres;

public static class PersistenceModule
{
    public static IServiceCollection AddPersistenceModule(this IServiceCollection services)
    {
        services.AddSingleton<IDbContext, DbContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<INewsLineRepository, NewsLineRepository>();
        
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        return services;
    }
}