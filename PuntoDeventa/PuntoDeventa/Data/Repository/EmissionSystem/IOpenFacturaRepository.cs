using PuntoDeventa.UI.Sales.Models;
using PuntoDeventa.UI.Sales.State;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeventa.Data.Repository.EmissionSystem
{
    public interface IOpenFacturaRepository
    {
        Task<SalesState> ToEmitDte(PaymentSales paymentSales);
    }
}
