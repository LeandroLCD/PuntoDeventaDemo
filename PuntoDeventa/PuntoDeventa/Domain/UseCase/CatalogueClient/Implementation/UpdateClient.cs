namespace PuntoDeventa.Domain.UseCase.CatalogueClient.Implementation
{
    using PuntoDeventa.Data.Repository.CatalogueClient;
    using PuntoDeventa.UI.CatalogueClient.Model;
    using PuntoDeventa.UI.CatalogueClient.States;
    using System.Threading.Tasks;
    using Xamarin.Forms;
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
