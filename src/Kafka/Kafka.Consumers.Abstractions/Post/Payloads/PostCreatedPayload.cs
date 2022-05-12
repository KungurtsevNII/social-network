namespace Kafka.Consumers.Abstractions.Post;

public sealed record PostCreatedPayload(
    long UserId,
    Guid PostId,
    string PostText);