namespace PuntoDeVenta.Maui.Domain.UseCase.CatalogueClient.Implementation
{
    using PuntoDeVenta.Maui.Data.Repository.CatalogueClient;
    using PuntoDeVenta.Maui.UI.CatalogueClient.Model;
    using PuntoDeVenta.Maui.UI.CatalogueClient.States;
    using System.Threading.Tasks;
    internal class UpdateClient : BaseCatalogueClientUseCase, IUpdateClient
    {
        private ICatalogueClientRepository _repository;

        public UpdateClient()
        {
            _repository = DependencyService.Get<ICatalogueClientRepository>();
        }

        public async Task<CatalogeState> UpDateClient(Client client)
        {
            return await MakeCallUseCase(client, async () =>
            {
                return await _repository.UpDateClient(client);
            });
        }
    }
}
