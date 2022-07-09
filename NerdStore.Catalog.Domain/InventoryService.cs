using NerdStore.Catalog.Domain.Events;
using NerdStore.Catalog.Domain.Interfaces;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.Events;

namespace NerdStore.Catalog.Domain
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductRepository _productRepository;
        private readonly IDomainEventManager _domainEventManager;

        public InventoryService(IProductRepository productRepository, IDomainEventManager domainEventHandler)
        {
            _productRepository = productRepository;
            _domainEventManager = domainEventHandler;
        }

        public async Task ReplenishStock(Guid productId, int quantity)
        {
            productId.ExceptionIfEmpty("Product ID can't be empty.");
            quantity.ExceptionIfLessThan(1, "Quantity must to be greater than zero.");

            var product = await _productRepository.GetById(productId);
            product.ExceptionIfNull("Product not found.");

            product.ReplenishStock(quantity);

            _productRepository.Update(product);
            await _productRepository.Commit();
        }

        public async Task WithdrawFromStock(Guid productId, int quantity)
        {
            productId.ExceptionIfEmpty("Product ID can't be empty.");
            quantity.ExceptionIfLessThan(1, "Quantity must to be greater than zero.");

            var product = await _productRepository.GetById(productId);
            product.ExceptionIfNull("Product not found.");

            product.WithdrawFromStock(quantity);

            if (product.Quantity <= 0)
                _domainEventManager.PublishEvent(new ProductOutOfStockEvent(productId));

            _productRepository.Update(product);
            await _productRepository.Commit();
        }        
    }
}
