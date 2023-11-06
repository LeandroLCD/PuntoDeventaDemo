using PuntoDeventa.UI.CatalogueClient.Model;
using PuntoDeventa.UI.CategoryProduct.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PuntoDeventa.UI.Sales.Models
{
    public class Sale
    {
        [Required(ErrorMessage = "La Fecha es requerida")]
        public DateTime DateSale { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un cliente")]
        public Client Client { get; set; }

        public EconomicActivities SelectEconomicActivities { get; set; }

        public BranchOffices SelectBranchOffices { get; set; }

        public int TotalNeto => (int)Math.Floor(Products.Sum(p => p.SubTotal));

        //TODO iva from Firebase


        [CustomValidation(typeof(Sale), "ValidateProducts")]
        public IEnumerable<ProductSales> Products { get; set; }

        private static ValidationResult ValidateProducts(IEnumerable<ProductSales> products, ValidationContext validationContext)
        {
            if (products == null || !products.Any())
            {
                return new ValidationResult("Debes agregar al menos un producto a la venta.");
            }

            return ValidationResult.Success;
        }

        public int TotalSale(double iva)
        {
            return (int)Math.Floor(d: TotalNeto * (1 + iva));
        }

    }
}
