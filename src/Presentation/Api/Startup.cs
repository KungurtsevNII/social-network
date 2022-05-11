using Api.Extensions;
using Api.Services;
using Application;
using Application.Services;
using FluentMigrator.Runner;
using Kafka.Producers;
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
            .AddPersistenceModule()
            .AddKafkaProducers(builder.Configuration)
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
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMigrationsModule(app.Configuration);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        using (var scope = serviceProvider.CreateScope())
        {
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        var serviceCollectionReplica = new ServiceCollection();
        serviceCollectionReplica.AddReplicaMigrationsModule(app.Configuration);
        var serviceProviderReplica = serviceCollectionReplica.BuildServiceProvider();
        
        using (var scope = serviceProviderReplica.CreateScope())
        {
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        return app;
    }
}