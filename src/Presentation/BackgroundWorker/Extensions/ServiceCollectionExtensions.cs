using BackgroundWorker.HostedServices;

namespace BackgroundWorker.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddHostedServices(this IServiceCollection services) =>
        services
            .AddHostedService<KafkaConsumers>()
        ;
}