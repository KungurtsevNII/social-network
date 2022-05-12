using Kafka.Consumers.Abstractions.Base;

namespace Kafka.Consumers.Post;

public sealed class PostConsumerOptions : ConsumerOptionsBase
{
    public const string OptionsPath = "PostConsumer";
}