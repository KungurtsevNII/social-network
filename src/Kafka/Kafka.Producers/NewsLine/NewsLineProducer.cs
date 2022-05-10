using System.Text.Json;
using Confluent.Kafka;
using Kafka.Producers.Abstractions.Base;
using Kafka.Producers.Abstractions.NewsLine;
using Microsoft.Extensions.Options;

namespace Kafka.Producers.NewsLine;

public sealed class NewsLineProducer : INewsLineProducer
{
    private readonly NewsLineProducerOptions _options;

    public NewsLineProducer(IOptions<NewsLineProducerOptions> options)
    {
        _options = options.Value ?? throw new ArgumentNullException(nameof(_options));
    }

    public async Task ProduceAsync(string key, KafkaMessage? message, CancellationToken ct)
     
    {
        using var producer = new ProducerBuilder<string, string>(_options.KafkaOptions).Build();
        try
        {
            var produceResult =  await producer.ProduceAsync(
                _options.Topic, 
                new Message<string, string>
                {
                    Key = key,
                    Value = JsonSerializer.Serialize(message)
                }, ct);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Oops, something went wrong: {e}");
        }
    }
}