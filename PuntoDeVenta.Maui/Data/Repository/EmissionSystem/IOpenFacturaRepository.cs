using PuntoDeVenta.Maui.UI.Sales.Models;
using PuntoDeVenta.Maui.UI.Sales.State;

namespace PuntoDeVenta.Maui.Data.Repository.EmissionSystem
{
    public interface IOpenFacturaRepository
    {
        Task<SalesState> CreatePreviewPdf(PaymentSales sales);
        Task<SalesState> EmitFactura(PaymentSales paymentSales);
        Task<SalesState> InsertNotaDePedido(PaymentSales paymentSales);
        Task<SalesState> SyncInformationTributary();
    }
}
