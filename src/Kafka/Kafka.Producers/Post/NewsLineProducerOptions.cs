using Kafka.Producers.Abstractions.Base;

namespace Kafka.Producers.Post;

public sealed class NewsLineProducerOptions : ProducerOptionsBase
{
    public const string OptionsPath = "NewsLineKafkaProducer";
}