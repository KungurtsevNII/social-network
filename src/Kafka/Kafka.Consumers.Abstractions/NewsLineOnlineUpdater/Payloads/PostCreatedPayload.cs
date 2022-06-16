namespace Kafka.Consumers.Abstractions.NewsLineOnlineUpdater.Payloads;

public sealed record PostCreatedPayload(
    long UserId,
    Guid PostId,
    string PostText);