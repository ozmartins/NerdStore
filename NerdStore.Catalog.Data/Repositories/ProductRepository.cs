using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NerdStore.Catalog.Data.Models;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.Interfaces;

namespace NerdStore.Catalog.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _catalogContext;

        private readonly IMapper _mapper;

        public ProductRepository(CatalogContext catalogContext, IMapper mapper)
        {
            _catalogContext = catalogContext;
            _mapper = mapper;
        }

        public async Task Commit()
        {
            await _catalogContext.SaveChangesAsync();
        }        

        public async Task<IEnumerable<Product>> GetAll()
        {
            var productModelList = await _catalogContext
                .Products!
                .AsNoTracking()
                .Include(x => x.Category)
                .ToListAsync();            

            return _mapper.Map<IEnumerable<Product>>(productModelList);
        }

        public async Task<IEnumerable<Product>> GetByCategory(Guid categoryId)
        {
            var products = await _catalogContext.Products!
                    .AsNoTracking()
                    .Include(x => x.Category)
                    .Where(x => x.Category!.Id.Equals(categoryId))
                    .ToListAsync();

            return _mapper.Map<IEnumerable<Product>>(products);
        }

        public async Task<Product> GetById(Guid id)
        {
            return _mapper
                .Map<Product>(await _catalogContext.Products!
                .FindAsync(id))!;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return _mapper
                .Map<IEnumerable<Category>>(await _catalogContext.Categories!
                .AsNoTracking()
                .ToListAsync());
        }

        public void Add(Product product)
        {
            var productModel = _mapper.Map<ProductModel>(product);

            _catalogContext.Products!.Add(productModel);
        }

        public void Update(Product product)
        {
            var savedProductModel = _catalogContext.Products!.Find(product.Id);

            _catalogContext.Entry(savedProductModel!).CurrentValues.SetValues(product); //update just the changed fields.
        }
        public void Add(Category category)
        {
            var categoryModel = _mapper.Map<CategoryModel>(category);

            _catalogContext.Categories!.Add(categoryModel);
        }        
    }
}
