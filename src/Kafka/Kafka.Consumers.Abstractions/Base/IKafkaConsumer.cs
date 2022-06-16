
namespace Kafka.Consumers.Abstractions.Base;
public interface IKafkaConsumer
{
    Task ConsumeAsync(CancellationToken ct);
}
