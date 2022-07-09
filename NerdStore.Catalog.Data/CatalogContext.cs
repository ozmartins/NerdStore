using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NerdStore.Catalog.Data.Models;

namespace NerdStore.Catalog.Data
{
    public class CatalogContext : DbContext
    {
        public DbSet<ProductModel>? Products { get; private set; }
        public DbSet<CategoryModel>? Categories { get; private set; }

        public CatalogContext() : base() { }

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