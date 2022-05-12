using Confluent.Kafka;
using Kafka.Producers.Abstractions.Post;
using Microsoft.Extensions.Options;

namespace Kafka.Producers.Post;

public sealed class NewsLineProducer : IPostProducer
{
    private readonly NewsLineProducerOptions _options;

    public NewsLineProducer(IOptions<NewsLineProducerOptions> options)
    {
        _options = options.Value ?? throw new ArgumentNullException(nameof(_options));
    }

    public async Task ProduceAsync(string key, string message, CancellationToken ct)
    {
        using var producer = new ProducerBuilder<string, string>(_options.KafkaOptions).Build();
        try
        {
            await producer.ProduceAsync(
                _options.Topic, 
                new Message<string, string>
                {
                    Key = key,
                    Value = message
                }, ct);
        }
        catch (Exception e)
        {
            Console.WriteLine($"something went wrong: {e}");
        }
    }
}