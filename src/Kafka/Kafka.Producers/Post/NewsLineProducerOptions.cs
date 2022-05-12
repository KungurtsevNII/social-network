namespace Kafka.Producers.Post;

public sealed class NewsLineProducerOptions
{
    public const string OptionsPath = "NewsLineKafkaProducer";

    public Dictionary<string, string> KafkaOptions { get; set; } = new();
    
    public string Topic { get; set; } = string.Empty;
}