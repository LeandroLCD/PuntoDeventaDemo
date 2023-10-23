using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PuntoDeventa.UI.CatalogueClient.Model;
using PuntoDeventa.UI.CategoryProduct.Models;

namespace PuntoDeventa.UI.Sales.Models
{
    internal class Sale
    {
        public DateTime DateSale{ get; set; }

        public Client Client{ get; set; }

        public EconomicActivities SelectEconomicActivities { get; set; }

        public BranchOffices SelectBranchOffices { get; set; }

        public double TotalNeto => Products.Sum(p => p.SubTotal);

        //TODO iva from Firebase
        public double TotalSale => Products.Sum(p => p.SubTotal) * 1.19;

        public IEnumerable<ProductSales> Products { get; set; }
    }
}
