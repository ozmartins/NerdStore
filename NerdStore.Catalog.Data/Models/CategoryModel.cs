namespace NerdStore.Catalog.Data.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string? Name { get; set; }        
        public ICollection<ProductModel>? Products { get; set; }               
    }
}
