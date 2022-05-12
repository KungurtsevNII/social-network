using System.Text.Json.Nodes;

namespace Kafka.Consumers.Abstractions;

public sealed record KafkaMessage(
    string EventType,
    DateTime OccuredAt,
    JsonObject Payload);