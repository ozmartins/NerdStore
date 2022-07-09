using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalog.Data.Models;
using NerdStore.Catalog.Domain.Entities;

namespace NerdStore.Catalog.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<CategoryModel>
    {
        public void Configure(EntityTypeBuilder<CategoryModel> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired();

            builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(250)");

            //builder.HasMany(c => c.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);
        }
    }
}
