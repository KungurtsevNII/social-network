using Kafka.Producers.Abstractions.Base;

namespace Kafka.Producers.Abstractions.NewsLine;

public interface INewsLineProducer
{
    Task ProduceAsync(string key, KafkaMessage? message, CancellationToken ct);
}