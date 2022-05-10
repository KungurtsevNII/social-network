using Kafka.Producers.Abstractions.Base;

namespace Kafka.Producers.Abstractions.NewsLine.Payloads;

public sealed record PostCreatedPayload(
    long UserId,
    long PostId,
    string PostText) : IPayload;