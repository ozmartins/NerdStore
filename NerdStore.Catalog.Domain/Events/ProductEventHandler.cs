using MediatR;
using Microsoft.Extensions.Logging;
using NerdStore.Catalog.Domain.Interfaces;

namespace NerdStore.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<ProductOutOfStockEvent>
    {
        private readonly ILogger _logger;
        private readonly IProductRepository _productRepository;

        public ProductEventHandler(ILogger logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task Handle(ProductOutOfStockEvent notification, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(notification.AggregateId);

            _logger.Log(LogLevel.Information, "Product {Name} is out of stock", product.Name);
        }
    }
}
