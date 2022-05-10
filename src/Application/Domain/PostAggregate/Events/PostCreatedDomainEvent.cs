using SharedKernel;

namespace Domain.PostAggregate.Events;

public sealed record PostCreatedDomainEvent(
    Guid PostId,
    long UserId,
    string Text) : DomainEventBase;
