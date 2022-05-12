namespace Kafka.Producers.Abstractions.Base;

public sealed record KafkaMessage<TPayload>(
    string EventType,
    DateTime OccuredAt,
    TPayload Payload) where TPayload : IPayload;