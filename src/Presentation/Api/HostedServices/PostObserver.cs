using Kafka.Consumers.Abstractions.NewsLineOnlineUpdater;

namespace Api.HostedServices;

public class PostObserver : BackgroundService
{
    private readonly INewsLineOnlineUpdaterConsumer _consumer;

    public PostObserver(INewsLineOnlineUpdaterConsumer consumer)
    {
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Task.Run(async () => await _consumer.ConsumeAsync(stoppingToken));
    }
}