using MediatR;
using SharedKernel;

namespace Application.Services;

public sealed class MediatorEventBus : IEventBus
{
    private readonly IMediator _mediator;

    public MediatorEventBus(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public Task PublishAsync(DomainEventBase @event, CancellationToken cancellationToken)
    {
        return _mediator.Publish(@event, cancellationToken);
    }

    public async Task PublishAllAsync(IEnumerable<DomainEventBase> events, CancellationToken cancellationToken)
    {
        foreach (var @event in events)
        {
            await PublishAsync(@event, cancellationToken).ConfigureAwait(false);
        }
    }
}