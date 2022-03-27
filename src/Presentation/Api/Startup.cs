using Api.Extensions;
using Api.Services;
using Application;
using Application.Services;
using FluentMigrator.Runner;
using Persistence.Postgres;
using Persistence.Postgres.Migrations;
using Services.Auth;

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
            .AddScoped<ICurrentUserContext, CurrentUserContext>()
            .AddHttpContextAccessor()
            .AddApplicationModule()
            .AddAuthServicesModule()
            .AddMigrationsModule(builder.Configuration)
            .AddPersistenceModule()
            .AddSwaggerDocumentation()
            .AddJwtTokenAuthentication(builder.Configuration);

        return builder;
    }

    private static WebApplication ConfigureMiddlewares(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        
        app.UseAuthentication();
        app.UseAuthorization();
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