using Kafka.Producers.Abstractions.Base;
using Kafka.Producers.Abstractions.Message;

namespace Kafka.Producers.Abstractions.Post.Payloads;

public sealed record PostCreatedPayload(
    long UserId,
    Guid PostId,
    string PostText) : IPayload;