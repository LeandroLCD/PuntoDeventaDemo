using PuntoDeventa.Domain.Helpers;

namespace PuntoDeventa.UI.CategoryProduct.Models
{

    public class ProductSales : Product
    {
        public ProductSales()
        {

        }
        public ProductSales(Product product, int quantity = 1)
        {
            this.CopyPropertiesFrom(product);
            Quantity = quantity;
        }
        public int Quantity { get; set; }
        public double SubTotal => PriceNeto * Quantity;

        public override bool Equals(object obj)
        {
            if (obj is ProductSales other)
            {
                return Id == other.Id &&
                    Name == other.Name &&
                    Quantity == other.Quantity &&
                    BarCode == other.BarCode;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Sku.GetHashCode();
        }
    }
}
