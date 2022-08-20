using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NerdStore.Catalog.Data.Models;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace NerdStore.Catalog.Data
{
    [ExcludeFromCodeCoverage]
    public class CatalogContext : DbContext
    {
        public DbSet<ProductModel>? Products { get; set; }
        public DbSet<CategoryModel>? Categories { get; set; }

        public CatalogContext()
        {
        }

        public CatalogContext(DbContextOptions options) : base(options) { }        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseLoggerFactory(LoggerFactory.Create(x => x.AddConsole()))
            .EnableSensitiveDataLogging();
        }
    }
}