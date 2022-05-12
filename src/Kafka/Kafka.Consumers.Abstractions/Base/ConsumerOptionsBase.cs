namespace Kafka.Consumers.Abstractions.Base;

public abstract class ConsumerOptionsBase
{
    public Dictionary<string, string> KafkaOptions { get; set; } = new();
    
    public string Topic { get; set; } = string.Empty;
}