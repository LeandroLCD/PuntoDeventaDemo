namespace PuntoDeventa.Domain.UseCase.CatalogueClient.Implementation
{
    using PuntoDeventa.Data.Repository.CatalogueClient;
    using PuntoDeventa.UI.CatalogueClient.Model;
    using PuntoDeventa.UI.CatalogueClient.States;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    internal class DeleteRouteUseCase : BaseCatalogueClientUseCase, IDeleteRouteUseCase
    {
        private ICatalogueClienteRepository _repository;

        public DeleteRouteUseCase()
        {
            _repository = DependencyService.Get<ICatalogueClienteRepository>();
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
