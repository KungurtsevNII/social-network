using Api.Extensions;
using Api.HostedServices;
using Api.Services;
using Application;
using Application.Services;
using FluentMigrator.Runner;
using Hubs;
using Kafka.Consumers;
using Kafka.Producers;
using Microsoft.AspNetCore.SignalR;
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
            .AddSignalR().Services
            .AddScoped<ICurrentUserContext, CurrentUserContext>()
            .AddHttpContextAccessor()
            .AddCustomCors()
            .AddApplicationModule(builder.Configuration)
            .AddAuthServicesModule()
            .AddPersistenceModule()
            .AddKafkaProducers(builder.Configuration)
            .AddKafkaConsumers(builder.Configuration)
            .AddSwaggerDocumentation()
            .AddJwtTokenAuthentication(builder.Configuration)
            .AddSingleton<IUserIdProvider, CustomUserIdProvider>()
            .AddHostedService<PostObserver>()
            ;

        return builder;
    }

    private static WebApplication ConfigureMiddlewares(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("_allowSpecificOrigins");
        app.MapControllers();
        app.MapHub<PostHub>("/post-hub");
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