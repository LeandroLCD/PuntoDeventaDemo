using PuntoDeVenta.Maui.Domain.Helpers;
using PuntoDeVenta.Maui.UI.CategoryProduct.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PuntoDeVenta.Maui.UI.Reports.Models
{
    public class ReportSale : DataReport
    {
        public DateTime Date { get; set; }

        public string PdfBase64 { get; set; }

        public double TotalNet => Products.IsNull() ? 0 : Math.Floor(Products.Sum(p => p.SubTotal));

        public double Iva { get; set; }

        public double AmountIva => Iva == 0 ? 0 : Math.Floor(TotalNet * (1 + Iva));

        public IEnumerable<ProductSales> Products { get; set; }
    }
}
