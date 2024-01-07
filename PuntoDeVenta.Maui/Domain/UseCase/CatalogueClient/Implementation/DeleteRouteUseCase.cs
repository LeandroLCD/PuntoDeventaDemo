namespace PuntoDeVenta.Maui.Domain.UseCase.CatalogueClient.Implementation
{
    using PuntoDeVenta.Maui.Data.Repository.CatalogueClient;
    using PuntoDeVenta.Maui.UI.CatalogueClient.Model;
    using PuntoDeVenta.Maui.UI.CatalogueClient.States;
    using System.Threading.Tasks;

    internal class DeleteRouteUseCase : BaseCatalogueClientUseCase, IDeleteRouteUseCase
    {
        private readonly ICatalogueClientRepository _repository;

        public DeleteRouteUseCase()
        {
            _repository = DependencyService.Get<ICatalogueClientRepository>();
        }
        public async Task<CatalogeState> DeleteRoute(SalesRoutes route)
        {
            return await MakeCallUseCase(route, async () =>
            {
                return await _repository.DeleteRoute(route);
            });
        }
    }
}
