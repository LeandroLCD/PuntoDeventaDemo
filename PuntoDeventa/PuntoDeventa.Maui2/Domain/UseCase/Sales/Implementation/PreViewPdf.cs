using PuntoDeventa.Data.Repository.EmissionSystem;
using PuntoDeventa.UI.Sales.Models;
using PuntoDeventa.UI.Sales.State;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.Domain.UseCase.Sales.Implementation
{
    internal class PreViewPdf : IPreViewPdf
    {
        private readonly IOpenFacturaRepository _repository;

        public PreViewPdf()
        {
            _repository = DependencyService.Get<IOpenFacturaRepository>();
        }

        public PreViewPdf(IOpenFacturaRepository repository)
        {
            _repository = repository;
        }
        public Task<SalesState> ToCreate(PaymentSales sales)
        {
            return _repository.CreatePreviewPdf(sales);
        }
    }
}
