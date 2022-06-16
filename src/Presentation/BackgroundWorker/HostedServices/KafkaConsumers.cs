using Kafka.Consumers.Abstractions.Post;

namespace BackgroundWorker.HostedServices;

public class KafkaConsumers : BackgroundService
{
    private readonly IPostConsumer _consumer;

    public KafkaConsumers(IPostConsumer consumer)
    {
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _consumer.ConsumeAsync(stoppingToken);
    }
}