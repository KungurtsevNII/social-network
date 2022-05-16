using Application;
using BackgroundWorker.Extensions;
using Kafka.Consumers;
using Kafka.Producers;
using Persistence.Postgres;
using Services.Auth;

namespace BackgroundWorker;

public static class Startup
{
    public static IHost InitializeApp(string[] args) =>
        Host
            .CreateDefaultBuilder(args)
            .ConfigureService()
            .Build();
    
    private static IHostBuilder ConfigureService(this IHostBuilder builder) => 
        builder
            .ConfigureServices((context, services) => 
                services
                    .AddHostedServices()
                    .AddAuthServicesModule()
                    .AddKafkaProducers(context.Configuration)
                    .AddKafkaConsumers(context.Configuration)
                    .AddApplicationModule(context.Configuration)
                    .AddPersistenceModule());
}