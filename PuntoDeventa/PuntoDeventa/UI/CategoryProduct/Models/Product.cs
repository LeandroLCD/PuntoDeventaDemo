namespace PuntoDeventa.UI.CategoryProduct.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class Product
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es requerido.")]
        public string Name { get; set; }
        public string Description { get; set; }

        public int BarCode { get; set; }

        [Required(ErrorMessage = "El Sku es requerido.")]
        public int Sku { get; set; }
        [Required(ErrorMessage = "La unidad de medida es requerido.")]
        public string UDM { get; set; }

        public string SkuCode => $"{Sku}_{UDM}";
        public bool IsOffer { get; set; }
        public bool InReport { get; set; }

        [RegularExpression(@"^(100(\.0{1,2})?|\d{0,2}(\.\d{1,2})?)$", ErrorMessage = "El Porcentaje de oferta debe estar entre 0 y 100.")]
        public float Percentage { get; set; }

        public double PriceOffer => IsOffer && Percentage > 0 ? Math.Round(PriceGross - (PriceGross * (Percentage / 100)), 0) : PriceGross;
        public double PriceNeto => Math.Round(PriceOffer + (PriceOffer * (IVA/ 100)));
        public float IVA { get; set; }

        public int Stock { get; set; }

        [Required(ErrorMessage = "El Precio es requerido.")]
        public double PriceGross { get; set; }

        [Required(ErrorMessage = "El ID de la categoria es requerido.")]
        public string CategoryId { get; set; }

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

