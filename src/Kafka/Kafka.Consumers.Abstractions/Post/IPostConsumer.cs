namespace Kafka.Consumers.Abstractions.Post;

public interface IPostConsumer
{
    Task ConsumeAsync(CancellationToken ct);
}