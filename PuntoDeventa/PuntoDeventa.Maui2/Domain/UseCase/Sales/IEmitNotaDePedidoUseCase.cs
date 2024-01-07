using PuntoDeventa.UI.Sales.Models;
using PuntoDeventa.UI.Sales.State;
using System.Threading.Tasks;

namespace PuntoDeventa.Domain.UseCase.Sales
{
    public interface IEmitNotaDePedidoUseCase
    {
        Task<SalesState> DoEmit(PaymentSales sales);
    }
}
