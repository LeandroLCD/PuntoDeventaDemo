namespace PuntoDeventa.UI.CategoryProduct.Models
{
    using System.ComponentModel.DataAnnotations;
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
        public bool IsOffer { get; set; }
        public bool InReport { get; set; }
        public float Percentage { get; set; }

        public double PriceOffer => IsOffer ? (PriceNeto * Percentage) + PriceGross : PriceNeto;
        public double PriceNeto => PriceGross + (PriceGross * IVA);
        public float IVA { get; set; }

        [Required(ErrorMessage = "El Precio es requerido.")]
        public double PriceGross { get; set; }

        [Required(ErrorMessage = "El ID de la categoria es requerido.")]
        public string CategoryId { get; set; }
    }
}

