using Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Postgres.Repositories;

namespace Persistence.Postgres;

public static class PersistenceModule
{
    public static IServiceCollection AddPersistenceModule(this IServiceCollection services) => 
        services
            .AddScoped<IUserStore<User>, UserRepository>()
            .AddScoped<IRoleStore<Role>, RoleRepository>()
        ;
}