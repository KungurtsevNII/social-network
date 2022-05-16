namespace Kafka.Consumers.Abstractions.Post.Payloads;

public sealed record PostCreatedPayload(
    long UserId,
    Guid PostId,
    string PostText);