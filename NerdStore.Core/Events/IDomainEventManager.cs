namespace NerdStore.Core.Events
{
    public interface IDomainEventManager
    {
        void PublishEvent(DomainEvent domainEvent);        
    }
}
