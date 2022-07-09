using MediatR;

namespace NerdStore.Core.Events
{
    public class DomainEvent : INotification
    {
        public string Type { get; private set; }
        public Guid AggregateId { get; private set; }
        public DateTime TimeStamp { get; private set; }

        public DomainEvent(Guid aggregateId)
        {
            Type = GetType().Name;
            AggregateId = aggregateId;
            TimeStamp = DateTime.Now;
        }
    }
}
