namespace Kafka.Producers.Abstractions.Base;

public sealed record KafkaMessage(
    string EventType,
    DateTime OccuredAt,
    IPayload Payload);