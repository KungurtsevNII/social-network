using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Postgres.Migrations.Migrations;

namespace Persistence.Postgres.Migrations;

public static class MigrationsModule
{
    public static IServiceCollection AddMigrationsModule(this IServiceCollection services, IConfiguration cfg) =>
        services
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(cfg.GetConnectionString("Main"))
                .WithMigrationsIn(typeof(AddIdentityTables).Assembly)
            )
            .AddLogging(lb => lb.AddFluentMigratorConsole())
        ;
    
    public static IServiceCollection AddReplicaMigrationsModule(this IServiceCollection services, IConfiguration cfg) =>
        services
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(cfg.GetConnectionString("Replication"))
                .WithMigrationsIn(typeof(AddIdentityTables).Assembly)
            )
            .AddLogging(lb => lb.AddFluentMigratorConsole())
    ;
}