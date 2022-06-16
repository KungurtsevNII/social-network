using Kafka.Consumers.Abstractions.Base;
using Kafka.Consumers.Abstractions.NewsLineOnlineUpdater;
using Kafka.Consumers.Abstractions.Post;
using Kafka.Consumers.NewsLineOnlineUpdater;
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

        services.Configure<NewsLineOnlineUpdaterConsumerOptions>(cfg.GetSection(NewsLineOnlineUpdaterConsumerOptions.OptionsPath));
        services.AddSingleton<INewsLineOnlineUpdaterConsumer, NewsLineOnlineUpdaterConsumer>();

        return services;
    }
}