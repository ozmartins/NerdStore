using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Entities
{
    public class Dimensions
    {
        public decimal Height { get; }
        public decimal Width { get; }
        public decimal Depth { get; }

        public Dimensions(decimal height, decimal width, decimal depth)
        {
            Height = height;
            Width = width;
            Depth = depth;

            Validate();
        }

        private void Validate()
        {
            Height.ExceptionIfLessThan(1, "Height cant't be less than one.");
            Width.ExceptionIfLessThan(1, "Width cant't be less than one.");
            Depth.ExceptionIfLessThan(1, "Depth cant't be less than one.");
        }

        public override bool Equals(object? obj)
        {
            var dimensions = (Dimensions)obj!;
            
            return dimensions.Height == Height && dimensions.Width == Width && dimensions.Depth == Depth;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Height, Width, Depth);
        }
    }
}