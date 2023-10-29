using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using PuntoDeventa.Data.Models;

namespace PuntoDeventa.UI.Sales.Models
{
    public class PaymentSales
    {
        public Sale Sale { get; set; }

        public DocumentType DocumentType { get; set; }


        public PaymentMethod PaymentMethod { get; set; }

        public IEnumerable<PaymentType> PaymentTypes { get; set; }
    }
}
