using PuntoDeVenta.Maui.Domain.Helpers;

namespace PuntoDeVenta.Maui.UI.CategoryProduct.Models
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

        public double SubTotalGross => PriceGross * Quantity;
        public override bool Equals(object obj)
        {
            if (obj is ProductSales other)
            {
                return Id == other.Id &&
                    Name == other.Name &&
                    Sku == other.Sku &&
                    BarCode == other.BarCode;
            }
            return false;
        }
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return Id.GetHashCode() ^ Sku.GetHashCode();
        }
    }
}
