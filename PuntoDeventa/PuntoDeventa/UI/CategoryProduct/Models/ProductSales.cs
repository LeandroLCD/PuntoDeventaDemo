using System;
using System.ComponentModel.DataAnnotations;

namespace PuntoDeventa.UI.CategoryProduct.Models
{

    public class ProductSales
    {
        public ProductSales()
        {

        }
        public ProductSales(Product product, int quantity = 1)
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
        public string Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es requerido.")]
        public string Name { get; set; }
        public string Description { get; set; }

        public int BarCode { get; set; }

        [Required(ErrorMessage = "El Sku es requerido.")]
        public int Sku { get; set; }
        [Required(ErrorMessage = "La unidad de medida es requerido.")]
        public string UDM { get; set; }
        public bool IsOffer { get; set; }
        public bool InReport { get; set; }

        [RegularExpression(@"^(100(\.0{1,2})?|\d{0,2}(\.\d{1,2})?)$", ErrorMessage = "El Porcentaje de oferta debe estar entre 0 y 100.")]
        public float Percentage { get; set; }

        public double PriceOffer => IsOffer && Percentage > 0 ? Math.Round(PriceGross - (PriceGross * (Percentage / 100)), 0) : PriceGross;
        public double PriceNeto => Math.Round(PriceOffer + (PriceOffer * (IVA / 100)));
        public float IVA { get; set; }

        public int Stock { get; set; }

        [Required(ErrorMessage = "El Precio es requerido.")]
        public double PriceGross { get; set; }

        [Required(ErrorMessage = "El ID de la categoria es requerido.")]
        public string CategoryId { get; set; }
        public int Quantity { get; set; }
        public double SubTotal => PriceNeto * Quantity;

        public override bool Equals(object obj)
        {
            if (obj is ProductSales other)
            {
                return Id == other.Id &&
                    SubTotal == other.SubTotal &&
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
