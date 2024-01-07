namespace PuntoDeVenta.Maui.Domain.UseCase.CatalogueClient.Implementation
{
    using PuntoDeVenta.Maui.Data.Repository.CatalogueClient;
    using PuntoDeVenta.Maui.UI.CatalogueClient.Model;
    using PuntoDeVenta.Maui.UI.CatalogueClient.States;
    using System.Threading.Tasks;

    internal class AddClientUseCase : BaseCatalogueClientUseCase, IAddClientUseCase
    {
        private readonly ICatalogueClientRepository _repository;

        public AddClientUseCase()
        {
            _repository = DependencyService.Get<ICatalogueClientRepository>();
        }
        public async Task<CatalogeState> Insert(Client client)
        {

            return await MakeCallUseCase(client, async () =>
            {
                return await _repository.InsertClient(client);
            });


        }
    }
}
