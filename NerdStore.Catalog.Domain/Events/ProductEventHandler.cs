using MediatR;

namespace NerdStore.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<ProductOutOfStockEvent>
    {
        public Task Handle(ProductOutOfStockEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
