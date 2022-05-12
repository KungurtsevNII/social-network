using Kafka.Producers.Abstractions.Base;

namespace Kafka.Producers.Abstractions.Message;

public sealed record KafkaMessage<TPayload>(
    string EventType,
    DateTime OccuredAt,
    TPayload Payload) where TPayload : IPayload;