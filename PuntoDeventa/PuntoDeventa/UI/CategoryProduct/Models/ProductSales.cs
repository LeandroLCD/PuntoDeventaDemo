using System;
using System.Collections.Generic;
using System.Text;

namespace PuntoDeventa.UI.CategoryProduct.Models
{
    
    public class ProductSales : Product
    {
        public ProductSales(Product product, int quantity = 1 )
        {
            Id = product.Id;
            Name = product.Name;
            Sku = product.Sku;
            BarCode = product.BarCode;
            Description = product.Description;
            UDM = product.UDM;
            IsOffer = product.IsOffer;
            InReport = product.InReport;
            Percentage = product.Percentage;
            CategoryId = product.CategoryId;
            PriceGross = product.PriceGross;
            IVA = product.IVA;
            Stock = product.Stock;
            Quantity = quantity;
        }

        public int Quantity {  get; set; }
        public double SubTotal => PriceNeto * Quantity;

        public override bool Equals(object obj)
        {
            if (obj is Product other)
            {
                return Id == other.Id &&
                    PriceNeto == other.PriceNeto &&
                    Name == other.Name &&
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
