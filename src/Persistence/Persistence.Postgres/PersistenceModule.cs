using Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Abstractions;
using Persistence.Postgres.IdentityStorages;

namespace Persistence.Postgres;

public static class PersistenceModule
{
    public static IServiceCollection AddPersistenceModule(this IServiceCollection services) => 
        services
            .AddSingleton<IDbContext, DbContext>()
            .AddScoped<IUserStore<User>, UserStorage>()
            .AddScoped<IRoleStore<Role>, RoleStorage>()
        ;
}