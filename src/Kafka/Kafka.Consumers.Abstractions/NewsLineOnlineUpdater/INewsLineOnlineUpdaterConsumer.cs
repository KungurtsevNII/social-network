namespace Kafka.Consumers.Abstractions.NewsLineOnlineUpdater;

public interface INewsLineOnlineUpdaterConsumer
{
    Task ConsumeAsync(CancellationToken ct);
}
