using AutoMapper;
using NerdStore.Catalog.Data.Models;
using NerdStore.Catalog.Domain.Entities;

namespace NerdStore.Catalog.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            _mapCategory();
            
            _mapProduct();            
        }

        private void _mapCategory()
        {
            CreateMap<Category, CategoryModel>();

            CreateMap<CategoryModel, Category>()
                .ConstructUsing(x => new Category(x.Id, x.Code, x.Name!));
        }
        
        private void _mapProduct()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(x => x.Height, y => y.MapFrom(z => z.Dimensions.Height))
                .ForMember(x => x.Width, y => y.MapFrom(z => z.Dimensions.Width))
                .ForMember(x => x.Depth, y => y.MapFrom(z => z.Dimensions.Depth))
                .ForMember(x => x.CategoryId, y => y.MapFrom(z => z.Category.Id))
                .ForMember(x => x.Category, y => y.MapFrom(z => new CategoryModel {  Id = z.Category.Id, Code = z.Category.Code, Name = z.Category.Name }))
                ;

            CreateMap<ProductModel, Product>()
                .ConstructUsing(x => new Product(
                    x.Id,
                    x.Name!,
                    x.Description!,
                    x.Image!,
                    x.Price,
                    x.Quantity,
                    new Dimensions(x.Height, x.Width, x.Depth),
                    new Category(x.Category!.Id, x.Category.Code, x.Category.Name!)));
        }        
    }
}
