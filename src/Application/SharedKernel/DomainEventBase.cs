using MediatR;

namespace SharedKernel;

public abstract record DomainEventBase(
    Guid EventId,
    DateTime OccuredAt) : INotification;