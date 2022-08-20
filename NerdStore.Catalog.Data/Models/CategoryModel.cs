using System.Diagnostics.CodeAnalysis;

namespace NerdStore.Catalog.Data.Models
{
    [ExcludeFromCodeCoverage]
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string? Name { get; set; }
        public IEnumerable<ProductModel>? Products { get; } = new List<ProductModel>();
    }
}
