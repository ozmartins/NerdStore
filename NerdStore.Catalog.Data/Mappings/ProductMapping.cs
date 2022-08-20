using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalog.Data.Models;

namespace NerdStore.Catalog.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(250);

            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);

            builder.Property(x => x.Image).IsRequired().HasMaxLength(250);

            builder.Property(x => x.Active).IsRequired();
            
            builder.Property(x => x.Price).IsRequired();
            
            builder.Property(x => x.Quantity).IsRequired();

            builder.Property(x => x.CreateDate).IsRequired();
            
            builder.Property(x => x.Height).IsRequired();
            
            builder.Property(x => x.Width).IsRequired();

            builder.Property(x => x.Depth).IsRequired();

            builder.Property(x => x.CategoryId).IsRequired();

            builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);
        }        
    }
}
