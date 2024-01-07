using PuntoDeVenta.Maui.Data.Models;
using System.Collections.Generic;

namespace PuntoDeVenta.Maui.UI.Sales.Models
{
    public class PaymentSales
    {
        public DteType DocumentType { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public IEnumerable<Payment> PaymentTypes { get; set; }

        public Sale Sale { get; set; }
    }
}
