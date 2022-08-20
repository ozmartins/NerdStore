using System.Diagnostics.CodeAnalysis;

namespace NerdStore.Catalog.Data.Models
{
    [ExcludeFromCodeCoverage]
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public bool Active { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateDate { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryModel? Category { get; set; }        
    }
}
