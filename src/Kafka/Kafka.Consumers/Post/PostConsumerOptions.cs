namespace Kafka.Consumers.Post;

public sealed class PostConsumerOptions
{
    public const string OptionsPath = "PostConsumer";
    
    public Dictionary<string, string> KafkaOptions { get; set; } = new();
    
    public string Topic { get; set; } = string.Empty;
}