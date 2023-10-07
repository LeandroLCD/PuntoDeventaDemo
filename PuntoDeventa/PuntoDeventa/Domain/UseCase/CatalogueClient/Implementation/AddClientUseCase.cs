namespace PuntoDeventa.Domain.UseCase.CatalogueClient.Implementation
{
    using PuntoDeventa.Data.Repository.CatalogueClient;
    using PuntoDeventa.UI.CatalogueClient.Model;
    using PuntoDeventa.UI.CatalogueClient.States;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    internal class AddClientUseCase : BaseCatalogueClientUseCase, IAddClientUseCase
    {
        private readonly ICatalogueClienteRepository _repository;

        public AddClientUseCase()
        {
            _repository = DependencyService.Get<ICatalogueClienteRepository>();
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
