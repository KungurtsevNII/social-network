namespace Kafka.Producers.Abstractions.Base;

public abstract class ProducerOptionsBase
{
    public Dictionary<string, string> KafkaOptions { get; set; } = new();
    
    public string Topic { get; set; } = string.Empty;
}