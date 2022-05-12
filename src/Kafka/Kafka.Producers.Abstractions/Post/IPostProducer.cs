namespace Kafka.Producers.Abstractions.Post;

public interface IPostProducer
{
    Task ProduceAsync(string key, string message, CancellationToken ct);
}