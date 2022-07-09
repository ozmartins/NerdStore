using NerdStore.Core.Events;

namespace NerdStore.Catalog.Domain.Events
{
    public class ProductOutOfStockEvent : DomainEvent
    {
        public ProductOutOfStockEvent(Guid aggregateId) : base(aggregateId)
        {            
        }
    }
}
