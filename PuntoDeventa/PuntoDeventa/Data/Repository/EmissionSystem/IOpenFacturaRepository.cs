using PuntoDeventa.UI.Sales.Models;
using PuntoDeventa.UI.Sales.State;
using System.Threading.Tasks;

namespace PuntoDeventa.Data.Repository.EmissionSystem
{
    public interface IOpenFacturaRepository
    {
        Task<SalesState> EmitFactura(PaymentSales paymentSales);
        Task<SalesState> InsertNotaDePedido(PaymentSales paymentSales);
        Task<SalesState> SyncInformationTributary();
    }
}
