using PuntoDeventa.Data.Repository.EmissionSystem;
using PuntoDeventa.UI.Sales.Models;
using PuntoDeventa.UI.Sales.State;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.Domain.UseCase.Sales.Implementation
{
    internal class SyncInformationTributaryUseCase : BaseSalesUseCase, ISyncInformationTributaryUseCase
    {
        private readonly IOpenFacturaRepository _repository;

        public SyncInformationTributaryUseCase()
        {
            _repository = DependencyService.Get<IOpenFacturaRepository>();
        }

        public async Task<SalesState> DoEmit(PaymentSales sales)
        {
            return await MakeCallUseCase(sales, async () => await _repository.InsertNotaDePedido(sales));
        }

        public async Task<SalesState> Sync()
        {
            return await _repository.SyncInformationTributary();
        }
    }
}
