namespace SharedKernel;

public interface IEventBus
{
    Task PublishAsync(DomainEventBase @event, CancellationToken cancellationToken);
    Task PublishAllAsync(IEnumerable<DomainEventBase> events, CancellationToken cancellationToken);
}