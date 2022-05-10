using MediatR;

namespace SharedKernel;

public abstract record DomainEventBase : INotification
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccuredAt { get; } = DateTime.UtcNow;
}