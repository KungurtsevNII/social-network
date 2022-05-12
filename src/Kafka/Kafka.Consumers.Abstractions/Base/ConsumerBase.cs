using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace Kafka.Consumers.Abstractions.Base;

public abstract class ConsumerBase
{
    protected readonly ILogger<ConsumerBase> Logger;

    protected ConsumerBase(ILogger<ConsumerBase> logger)
    {
        Logger = logger;
    }

    protected IConsumer<string, string> CreateConsumer(ConsumerOptionsBase consumerOptions)
    {
        return new ConsumerBuilder<string, string>(consumerOptions.KafkaOptions)
            .SetErrorHandler(
                (_, e) => Logger.LogError(
                    "[{Topic}] Consumer error: {ErrorCode},{ErrorReason}",
                    consumerOptions.Topic,
                    e.Code,
                    e.Reason))
            .SetPartitionsAssignedHandler(
                (_, partitions) =>
                    Logger.LogInformation(
                        "[{Topic}] Assigned partitions: [{Partitions}]",
                        consumerOptions.Topic,
                        string.Join(", ", partitions))
            )
            .SetPartitionsRevokedHandler(
                (_, partitions) =>
                    Logger.LogInformation(
                        "[{Topic}] Revoking assignment: [{Partitions}]",
                        consumerOptions.Topic,
                        string.Join(", ", partitions))
            ).Build();
    }
}