using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }        
        public string Image { get; private set; }
        public bool Active { get; private set; }
        public decimal Price { get; private set; }        
        public int QuantityInInventory { get; private set; }
        public DateTime CreateDate { get; private set; }
        public Dimensions Dimensions { get; set; }
        public Category Category { get; set; }

        public Product(string name, string description, string image, decimal price, int quantityInInventory, Dimensions dimensions, Category category)
        {
            Name = name;
            Description = description;
            Active = true;
            Price = price;
            CreateDate = DateTime.Now;
            Image = image;
            QuantityInInventory = quantityInInventory;
            Dimensions = dimensions;
            Category = category;
            
            Validate();
        }

        public void Activate() => Active = true;
        
        public void Deactivate() => Active = false;

        public void ChangeCategory(Category category)
        {
            ValidateCategory(category);
            Category = category;
        }

        public void ChangeDescription(string description) 
        {
            description.ExceptionIfEmpty("Description can't be empty.");
            Description = description;
        }
        public void DecreaseInventory(int quantity)
        {
            ValidateQuantity(quantity);

            if (!HasEnoughtInInventory(quantity)) 
                throw new Exception($"There are not enough items in inventory.");
            
            QuantityInInventory -= quantity;
        }

        public void IncreaseInventory(int quantity)
        {
            ValidateQuantity(quantity);

            QuantityInInventory += quantity;
        }

        public bool HasEnoughtInInventory(int quantity)
        {
            return QuantityInInventory >= quantity;
        }

        public void Validate()
        {
            Name.ExceptionIfEmpty("Name can't be empty.");
            Image.ExceptionIfEmpty("Image can't be empty.");
            Price.ExceptionIfLessThan(0.01m, "Price must to be greater than zero.");

            ValidateDescription(Description);
            ValidateQuantity(QuantityInInventory);
            ValidateCategory(Category);
        }

        public void ValidateDescription(string description)
        {
            description.ExceptionIfEmpty("Description can't be empty.");
        }

        public void ValidateQuantity(int quantity)
        {
            quantity.ExceptionIfLessThan(0, "Quantity must to be greater than zero.");
        }

        public void ValidateCategory(Category category)
        {
            category.ExceptionIfNull("Category can't be empty.");
            category.Id.ExceptionIfEqual(Guid.Empty, "Category Id can't be empty.");
        }
    }
}
