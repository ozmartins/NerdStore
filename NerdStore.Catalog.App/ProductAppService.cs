using NerdStore.Catalog.Domain;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.Interfaces;

namespace NerdStore.Catalog.App
{
    public class ProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryService _inventoryService;

        public ProductAppService(IProductRepository productRepository, IInventoryService inventoryService)
        {
            _productRepository = productRepository;
            _inventoryService = inventoryService;
        }

        public async Task<IEnumerable<Product>> GetAll()
        { 
            return await _productRepository.GetAll();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task<IEnumerable<Product>> GetByCategory(Guid categoryId)
        {
            return await _productRepository.GetByCategory(categoryId);
        }

        public async Task<IEnumerable<Category>> GetCategories() 
        {
            return await _productRepository.GetCategories();
        }

        public void Add(Product product)
        { 
            _productRepository.Add(product);
            _productRepository.Commit();
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
            _productRepository.Commit();
        }

        public async Task<Product> ReplenishStock(Guid productId, int quantity)
        { 
            await _inventoryService.ReplenishStock(productId, quantity);

            return await _productRepository.GetById(productId);
        }

        public async Task<Product> WithdrawFromStock(Guid productId, int quantity)
        {
            await _inventoryService.WithdrawFromStock(productId, quantity);

            return await _productRepository.GetById(productId);
        }
    }
}
