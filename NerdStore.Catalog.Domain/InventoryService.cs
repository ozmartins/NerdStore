using NerdStore.Catalog.Domain.Events;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.Events;

namespace NerdStore.Catalog.Domain
{
    public class InventoryService : IDisposable
    {
        private readonly IProductRepository _productRepository;
        private readonly DomainEventManager _domainEventManager;

        public InventoryService(IProductRepository productRepository, DomainEventManager domainEventHandler)
        {
            _productRepository = productRepository;
            _domainEventManager = domainEventHandler;
        }

        public async Task<bool> IncreaseInventory(Guid productId, int quantity)
        {
            productId.ExceptionIfEmpty("Product ID can't be empty.");
            quantity.ExceptionIfLessThan(1, "Quantity must to be greater than zero.");

            var product = await _productRepository.GetById(productId);            
            if (product is null) return false;

            product.IncreaseInventory(quantity);

            _productRepository.Update(product);
            return await _productRepository.Commit();
        }

        public async Task<bool> DecreaseInventory(Guid productId, int quantity)
        {
            productId.ExceptionIfEmpty("Product ID can't be empty.");
            quantity.ExceptionIfLessThan(1, "Quantity must to be greater than zero.");

            var product = await _productRepository.GetById(productId);
            if (product is null) return false;

            product.DecreaseInventory(quantity);

            if (product.Quantity <= 0)
                _domainEventManager.PublishEvent(new ProductOutOfStockEvent(productId));

            _productRepository.Update(product);
            return await _productRepository.Commit();
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
