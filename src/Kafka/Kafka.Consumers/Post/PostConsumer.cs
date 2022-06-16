using System.Text.Json;
using Application.Features.NewsLines.Command.UpdateUsersNewsLines;
using Kafka.Consumers.Abstractions;
using Kafka.Consumers.Abstractions.Base;
using Kafka.Consumers.Abstractions.Post;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PostCreatedPayload = Kafka.Consumers.Abstractions.NewsLineOnlineUpdater.Payloads.PostCreatedPayload;

namespace Kafka.Consumers.Post;

public sealed class PostConsumer : ConsumerBase, IPostConsumer, IKafkaConsumer
{
    private readonly PostConsumerOptions _options;
    private readonly IServiceProvider _serviceProvider;

    public PostConsumer(
        IOptions<PostConsumerOptions> options, 
        ILogger<PostConsumer> logger, 
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
            case PostEventType.Create:
            {
                var eventPayload = message.Payload.Deserialize<PostCreatedPayload>();
                if (eventPayload is null)
                {
                    throw new ArgumentNullException(nameof(eventPayload));
                }
                
                return new UpdateUsersNewsLinesCommand(eventPayload.UserId, eventPayload.PostId);
            }
            default:
            {
                Logger.LogError("Can not process message with type - {MessageType}", message.EventType);
                throw new ArgumentOutOfRangeException(nameof(KafkaMessage));
            }
        }
    }
}