namespace SharedKernel;

public abstract class AggregateRoot<T> : Entity<T>
{
    private List<DomainEventBase> _domainEvents;
    public IReadOnlyList<DomainEventBase> DomainEvents => _domainEvents;
    
    protected AggregateRoot(T id) : base(id)
    {
        _domainEvents = new List<DomainEventBase>();
    }
    
    public void AddDomainEvent(DomainEventBase eventItem)
    {
        _domainEvents.Add(eventItem);
    }
}