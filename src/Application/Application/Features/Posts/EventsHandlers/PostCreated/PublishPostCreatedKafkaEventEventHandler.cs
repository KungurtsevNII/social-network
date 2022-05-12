using System.Text.Json;
using Domain.PostAggregate.Events;
using Kafka.Producers.Abstractions.Message;
using Kafka.Producers.Abstractions.Post;
using Kafka.Producers.Abstractions.Post.Payloads;
using MediatR;

namespace Application.Features.Posts.EventsHandlers.PostCreated;

public sealed class PublishPostCreatedKafkaEventEventHandler : INotificationHandler<PostCreatedDomainEvent>
{
    private readonly IPostProducer _postProducer;

    public PublishPostCreatedKafkaEventEventHandler(IPostProducer postProducer)
    {
        _postProducer = postProducer;
    }

    public async Task Handle(PostCreatedDomainEvent domainEvent, CancellationToken ct)
    {
        var kafkaPayload = new PostCreatedPayload(domainEvent.UserId, domainEvent.PostId, domainEvent.Text);
        var kafkaMessage = new KafkaMessage<PostCreatedPayload>(PostEventType.Create, domainEvent.OccuredAt, kafkaPayload);

        await _postProducer.ProduceAsync(domainEvent.PostId.ToString(), JsonSerializer.Serialize(kafkaMessage), ct);
    }
}