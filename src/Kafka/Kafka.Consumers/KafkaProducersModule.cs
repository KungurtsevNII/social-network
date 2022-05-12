using Kafka.Consumers.Abstractions.Post;
using Kafka.Consumers.Post;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kafka.Consumers;

public static class KafkaConsumersModule
{
    public static IServiceCollection AddKafkaConsumers(this IServiceCollection services, IConfiguration cfg)
    {
        services.Configure<PostConsumerOptions>(cfg.GetSection(PostConsumerOptions.OptionsPath));
        services.AddSingleton<IPostConsumer, PostConsumer>();
        
        return services;
    }
}