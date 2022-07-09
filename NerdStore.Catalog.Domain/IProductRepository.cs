using NerdStore.Catalog.Domain.Entities;
using NerdStore.Core.Data;

namespace NerdStore.Catalog.Domain
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        
        Task<Product> GetById(Guid id);
        
        Task<IEnumerable<Product>> GetByCategory(Guid categoryId);
        
        Task<IEnumerable<Category>> GetCategories();
        
        void Add(Product product);

        void Update(Product product);
    }
}
