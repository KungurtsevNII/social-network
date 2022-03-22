using Domain.UserAggregate;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Identity;
using Persistence.Postgres;
using Persistence.Postgres.Migrations;
using Persistence.Postgres.Repositories;

namespace Api;

public static class Startup
{
    public static WebApplication InitializeApp(string[] args) => 
        WebApplication
            .CreateBuilder(args)
            .ConfigureService()
            .Build()
            .ConfigureMiddlewares()
            .UpdateDatabase();
    
    private static WebApplicationBuilder ConfigureService(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddControllers().Services
            .AddMigrationsModule(builder.Configuration)
            .AddPersistenceModule()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
        
        builder.Services
            .AddIdentity<User, Role>()
            .AddSignInManager<SignInManager<User>>()
            .AddDefaultTokenProviders();

        return builder;
    }

    private static WebApplication ConfigureMiddlewares(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.MapControllers();
        return app;
    }
    
    private static WebApplication UpdateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();

        return app;
    }
}