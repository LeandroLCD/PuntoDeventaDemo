using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PuntoDeVenta.Maui.UI.CatalogueClient.Model;
using PuntoDeVenta.Maui.UI.CategoryProduct.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PuntoDeVenta.Maui.UI.Sales.Models
{
    public class Sale
    {
        [JsonProperty("Date")]
        [Required(ErrorMessage = "La Fecha es requerida.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un cliente.")]
        public Client Client { get; set; }

        public EconomicActivities SelectEconomicActivities { get; set; }

        public BranchOffices SelectBranchOffices { get; set; }

        public int TotalNeto => (int)Math.Floor(Products.Sum(p => p.SubTotal));

        //TODO iva from Firebase


        [CustomValidation(typeof(Sale), "ValidateProducts")]
        public IEnumerable<ProductSales> Products { get; set; }

        public static ValidationResult ValidateProducts(IEnumerable<ProductSales> products, ValidationContext validationContext)
        {
            if (products == null || !products.Any())
            {
                return new ValidationResult("Debes agregar al menos un producto a la venta.", new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }

        public int TotalSale(double iva)
        {
            return (int)Math.Floor(d: TotalNeto * (1 + iva));
        }

    }
}
