using System.Text.Json;
using Application.Features.Posts.Command.UpdateUsersNewsLine;
using Confluent.Kafka;
using Kafka.Consumers.Abstractions;
using Kafka.Consumers.Abstractions.Post;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kafka.Consumers.Post;

public sealed class PostConsumer : IPostConsumer
{
    private readonly ILogger<PostConsumer> _logger;
    private readonly PostConsumerOptions _options;
    private readonly IServiceProvider _serviceProvider;

    public PostConsumer(
        IOptions<PostConsumerOptions> options, 
        ILogger<PostConsumer> logger, 
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _options = options.Value;
    }

    public async Task ConsumeAsync(CancellationToken ct)
    {
        using var consumer = Create();
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

    private IConsumer<string, string> Create()
    {
        return new ConsumerBuilder<string, string>(_options.KafkaOptions)
            .SetErrorHandler(
                (_, e) => _logger.LogError(
                    "[{Topic}] Consumer error: {ErrorCode},{ErrorReason}",
                    _options.Topic,
                    e.Code,
                    e.Reason))
            .SetPartitionsAssignedHandler(
                (_, partitions) =>
                    _logger.LogInformation(
                        "[{Topic}] Assigned partitions: [{Partitions}]",
                        _options.Topic,
                        string.Join(", ", partitions))
            )
            .SetPartitionsRevokedHandler(
                (_, partitions) =>
                    _logger.LogInformation(
                        "[{Topic}] Revoking assignment: [{Partitions}]",
                        _options.Topic,
                        string.Join(", ", partitions))
            ).Build();
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
                
                return new UpdateUsersNewsLineCommand(eventPayload.UserId, eventPayload.PostId);
            }
            default:
            {
                throw new ArgumentOutOfRangeException(nameof(KafkaMessage));
            }
        }
    }
}