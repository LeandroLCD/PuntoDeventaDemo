using PuntoDeventa.Data.Repository.CatalogueClient;
using PuntoDeventa.Data.Repository.CategoryProduct;
using PuntoDeventa.UI.CatalogueClient.Model;
using PuntoDeventa.UI.CatalogueClient.States;
using PuntoDeventa.UI.CategoryProduct.Models;
using PuntoDeventa.UI.CategoryProduct.States;
using System.Threading.Tasks;
namespace PuntoDeventa.Domain.UseCase.CatalogueClient.Implementation
{
    using Xamarin.Forms;

    internal class AddSalesRouteUseCase : BaseCatalogueClientUseCase, IAddSalesRouteUseCase
    {
        private ICatalogueClienteRepository _repository;

        public AddSalesRouteUseCase()
        {
            _repository = DependencyService.Get<ICatalogueClienteRepository>();
        }
        public async Task<CatalogeState> Insert(SalesRoutes route)
        {
            return await MakeCallUseCase(route, async () =>
            {
                return await _repository.InsertRoute(route);
            });

        }
    }
}
