using Microsoft.Maui;
namespace PuntoDeventa.Domain.UseCase.CatalogueClient.Implementation
{
    using PuntoDeventa.Data.Repository.CatalogueClient;
    using PuntoDeventa.UI.CatalogueClient.Model;
    using PuntoDeventa.UI.CatalogueClient.States;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    internal class AddSalesRouteUseCase : BaseCatalogueClientUseCase, IAddSalesRouteUseCase
    {
        private ICatalogueClientRepository _repository;

        public AddSalesRouteUseCase()
        {
            _repository = DependencyService.Get<ICatalogueClientRepository>();
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
