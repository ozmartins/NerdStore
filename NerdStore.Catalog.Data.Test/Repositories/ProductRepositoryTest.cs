using AutoMapper;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalog.Data.Repositories;
using NerdStore.Catalog.Domain.Entities;
using System;
using System.Linq;
using Xunit;

namespace NerdStore.Catalog.Data.Test.Repositories
{
    public class ProductRepositoryTest
    {
        private readonly IMapper _mapper;

        public ProductRepositoryTest()
        {
            var services = new ServiceCollection();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            var serviceProvider = services.BuildServiceProvider();

            _mapper = serviceProvider.GetService<IMapper>()!;
        }

        [Fact]
        public async void AddProductTest()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var contextOptions = new DbContextOptionsBuilder<CatalogContext>().UseSqlite(connection).Options;

            var context = new CatalogContext(contextOptions);

            await context.Database.EnsureCreatedAsync();

            var category1 = new Category(Guid.NewGuid(), 006, "Category");
            var category2 = new Category(Guid.NewGuid(), 060, "Category");
            var category3 = new Category(Guid.NewGuid(), 600, "Category");

            var product1 = new Product(Guid.NewGuid(), "Name1", "Description1", "Image1", 001, 002,
                new Dimensions(003, 004, 005), category1);
            var product2 = new Product(Guid.NewGuid(), "Name2", "Description2", "Image2", 010, 020,
                new Dimensions(030, 040, 050), category2);
            var product3 = new Product(Guid.NewGuid(), "Name3", "Description3", "Image3", 100, 200,
                new Dimensions(300, 400, 500), category3);

            var productRepository = new ProductRepository(context, _mapper);

            productRepository.Add(product1);
            productRepository.Add(product2);
            productRepository.Add(product3);

            await productRepository.Commit();

            var productList = (await productRepository.GetAll()).OrderBy(x => x.Name).ToList();

            productList.ElementAt(0).Id.Should().Be(product1.Id);
            productList.ElementAt(1).Id.Should().Be(product2.Id);
            productList.ElementAt(2).Id.Should().Be(product3.Id);
        }

        [Fact]
        public async void UpdateProductTest()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var contextOptions = new DbContextOptionsBuilder<CatalogContext>().UseSqlite(connection).Options;

            var context = new CatalogContext(contextOptions);

            await context.Database.EnsureCreatedAsync();

            var product1 = new Product(Guid.NewGuid(), "Name1", "Description1", "Image1", 001, 002,
                new Dimensions(003, 004, 005), new Category(Guid.NewGuid(), 006, "Category"));
            var product2 = new Product(Guid.NewGuid(), "Name2", "Description2", "Image2", 010, 020,
                new Dimensions(030, 040, 050), new Category(Guid.NewGuid(), 060, "Category"));
            var product3 = new Product(Guid.NewGuid(), "Name3", "Description3", "Image3", 100, 200,
                new Dimensions(300, 400, 500), new Category(Guid.NewGuid(), 600, "Category"));

            var productRepository = new ProductRepository(context, _mapper);

            productRepository.Add(product1);
            productRepository.Add(product2);
            productRepository.Add(product3);
            await productRepository.Commit();

            var foundProduct1 = await productRepository.GetById(product1.Id);
            var foundProduct2 = await productRepository.GetById(product2.Id);
            var foundProduct3 = await productRepository.GetById(product3.Id);

            foundProduct1.ChangeDescription("New description 1");
            foundProduct1.ChangeCategory(new Category(Guid.NewGuid(), 7, "New category 1"));

            foundProduct2.ChangeDescription("New description 2");
            foundProduct2.ChangeCategory(new Category(Guid.NewGuid(), 8, "New category 2"));

            foundProduct3.ChangeDescription("New description 3");
            foundProduct3.ChangeCategory(new Category(Guid.NewGuid(), 9, "New category 3"));

            productRepository.Update(foundProduct1);
            productRepository.Update(foundProduct2);
            productRepository.Update(foundProduct3);

            await productRepository.Commit();

            var changedProduct1 = await productRepository.GetById(product1.Id);
            var changedProduct2 = await productRepository.GetById(product2.Id);
            var changedProduct3 = await productRepository.GetById(product3.Id);

            changedProduct1.Id.Should().Be(product1.Id);
            changedProduct2.Id.Should().Be(product2.Id);
            changedProduct3.Id.Should().Be(product3.Id);
        }

        [Fact]
        public async void AddCategoryTest()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var contextOptions = new DbContextOptionsBuilder<CatalogContext>().UseSqlite(connection).Options;

            var context = new CatalogContext(contextOptions);

            await context.Database.EnsureCreatedAsync();

            var category = new Category(Guid.NewGuid(), 1, "Category");

            var productRepository = new ProductRepository(context, _mapper);

            productRepository.Add(category);
            await productRepository.Commit();

            var foundCategories = (await productRepository.GetCategories()).ToList();

            foundCategories.Should().HaveCount(1);
            foundCategories.First().Should().BeEquivalentTo(category);
        }

        [Fact]
        public async void GetByCategoriesTest()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var contextOptions = new DbContextOptionsBuilder<CatalogContext>().UseSqlite(connection).Options;

            var context = new CatalogContext(contextOptions);

            await context.Database.EnsureCreatedAsync();

            var category1 = new Category(Guid.NewGuid(), 6, "Categoria 1");
            var category2 = new Category(Guid.NewGuid(), 6, "Categoria 2");

            var product1 = new Product(Guid.NewGuid(), "Nome 1", "Descrição 1", "Image 1", 1.0m, 2, new Dimensions(3, 4, 5), category1);
            var product2 = new Product(Guid.NewGuid(), "Nome 2", "Descrição 2", "Image 2", 1.0m, 2, new Dimensions(3, 4, 5), category2);

            var productRepository = new ProductRepository(context, _mapper);

            productRepository.Add(product1);
            productRepository.Add(product2);

            await productRepository.Commit();

            var foundProduct = (await productRepository.GetByCategory(category1.Id)).ToList();

            foundProduct.Should().HaveCount(1);
            foundProduct.First().Id.Should().Be(product1.Id);
        }
    }
}