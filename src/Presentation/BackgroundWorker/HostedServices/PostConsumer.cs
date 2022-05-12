using Kafka.Consumers.Abstractions.Post;

namespace BackgroundWorker.HostedServices;

public class PostConsumer : BackgroundService
{
    private readonly IPostConsumer _consumer;

    public PostConsumer(IPostConsumer consumer)
    {
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _consumer.ConsumeAsync(stoppingToken);
    }
}