namespace Bookify.Domain.Abstractions;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];
    
    public Guid Id { get; }

    protected Entity(Guid id)
    {
        Id = id;
    }
    
    public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
        => _domainEvents.AsReadOnly();
    
    public void ClearDomainEvents()
        => _domainEvents.Clear();
    
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);
}