using PuntoDeventa.UI.Sales.Models;
using PuntoDeventa.UI.Sales.State;
using System.Threading.Tasks;

namespace PuntoDeventa.Domain.UseCase.Sales
{
    public interface IPreViewPdf
    {
        Task<SalesState> ToCreate(PaymentSales sales);
    }
}
