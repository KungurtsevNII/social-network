using Kafka.Producers.Abstractions.NewsLine;
using Kafka.Producers.NewsLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kafka.Producers;

public static class KafkaProducersModule
{
    public static IServiceCollection AddKafkaProducers(this IServiceCollection services, IConfiguration cfg)
    {
        services.Configure<NewsLineProducerOptions>(cfg.GetSection(NewsLineProducerOptions.OptionsPath));
        services.AddSingleton<INewsLineProducer, NewsLineProducer>();
        
        return services;
    }
}