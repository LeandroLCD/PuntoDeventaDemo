namespace PuntoDeVenta.Maui.Domain.UseCase.CatalogueClient.Implementation
{
    using PuntoDeVenta.Maui.Data.Repository.CatalogueClient;
    using PuntoDeVenta.Maui.UI.CatalogueClient.Model;
    using PuntoDeVenta.Maui.UI.CatalogueClient.States;
    using System.Threading.Tasks;

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
