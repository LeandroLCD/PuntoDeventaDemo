using PuntoDeventa.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PuntoDeventa.UI.CategoryProduct.Models
{
    public class Product
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoria es requerido.")]
        public string Name { get; set; }
        public string Description { get; set; }

        public int BarCode { get; set; }

        [Required(ErrorMessage = "El nombre de la categoria es requerido.")]
        public int Sku { get; set; }
        [Required(ErrorMessage = "El nombre de la categoria es requerido.")]
        public string UDM { get; set; }
        public bool IsOffer { get; set; }
        public float Percentage { get; set; }

        public double PriceOffer => IsOffer ? (PriceNeto * Percentage) + PriceGross : PriceNeto;
        public double PriceNeto => PriceGross + (PriceGross * IVA);
        public float IVA { get; set; }

        [Required(ErrorMessage = "El nombre de la categoria es requerido.")]
        public double PriceGross { get; set; }

        [Required(ErrorMessage = "El ID de la categoria es requerido.")]
        public string CategoryId { get; set; }
    }
}
