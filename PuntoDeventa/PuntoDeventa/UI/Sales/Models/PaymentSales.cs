using PuntoDeventa.Data.Models;
using System.Collections.Generic;

namespace PuntoDeventa.UI.Sales.Models
{
    public class PaymentSales
    {
        public Sale Sale { get; set; }

        public DteType DocumentType { get; set; }


        public PaymentMethod PaymentMethod { get; set; }

        public IEnumerable<PaymentType> PaymentTypes { get; set; }
    }
}
