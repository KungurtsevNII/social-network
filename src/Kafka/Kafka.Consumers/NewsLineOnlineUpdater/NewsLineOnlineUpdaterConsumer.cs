using Application.Features.NewsLines.Command.AddPostInNewLineOnline;
using Kafka.Consumers.Abstractions;
using Kafka.Consumers.Abstractions.Base;
using Kafka.Consumers.Abstractions.NewsLineOnlineUpdater;
using Kafka.Consumers.Abstractions.NewsLineOnlineUpdater.Payloads;
using Kafka.Consumers.Abstractions.Post;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Kafka.Consumers.NewsLineOnlineUpdater;

public class NewsLineOnlineUpdaterConsumer : ConsumerBase, INewsLineOnlineUpdaterConsumer, IKafkaConsumer
{
    private readonly NewsLineOnlineUpdaterConsumerOptions _options;
    private readonly IServiceProvider _serviceProvider;

    public NewsLineOnlineUpdaterConsumer(
        IOptions<NewsLineOnlineUpdaterConsumerOptions> options,
        ILogger<NewsLineOnlineUpdaterConsumer> logger,
        IServiceProvider serviceProvider) : base(logger)
    {
        _serviceProvider = serviceProvider;
        _options = options.Value;
    }

    public async Task ConsumeAsync(CancellationToken ct)
    {
        using var consumer = CreateConsumer(_options);
        consumer.Subscribe(_options.Topic);

        while (!ct.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(ct);
            var message = consumeResult.Message.Value;
            if (message is null)
            {
                continue;
            }

            var deserialezedMessage = JsonSerializer.Deserialize<KafkaMessage>(message);
            if (deserialezedMessage is null)
            {
                continue;
            }

            var mediatrRequest = ConvertToMediatrRequest(deserialezedMessage);
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(mediatrRequest, ct);

            consumer.Commit(consumeResult);
        }

        consumer.Close();
    }

    private IRequest ConvertToMediatrRequest(KafkaMessage message)
    {
        switch (message.EventType)
        {
            case NewsLineUpdaterEventType.Create:
                {
                    var eventPayload = message.Payload.Deserialize<PostCreatedPayload>();
                    if (eventPayload is null)
                    {
                        throw new ArgumentNullException(nameof(eventPayload));
                    }

                    return new AddPostInNewLineOnlineCommand(eventPayload.UserId, eventPayload.PostId);
                }
            default:
                {
                    Logger.LogError("Can not process message with type - {MessageType}", message.EventType);
                    throw new ArgumentOutOfRangeException(nameof(KafkaMessage));
                }
        }
    }
}