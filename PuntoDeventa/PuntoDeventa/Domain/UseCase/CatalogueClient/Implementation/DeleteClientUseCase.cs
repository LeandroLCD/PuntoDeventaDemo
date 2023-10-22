namespace PuntoDeventa.Domain.UseCase.CatalogueClient.Implementation
{
    using PuntoDeventa.Data.Repository.CatalogueClient;
    using PuntoDeventa.UI.CatalogueClient.Model;
    using PuntoDeventa.UI.CatalogueClient.States;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    internal class DeleteClientUseCase : BaseCatalogueClientUseCase, IDeleteClientUseCase
    {
        private readonly ICatalogueClienteRepository _repository;

        public DeleteClientUseCase()
        {
            _repository = DependencyService.Get<ICatalogueClienteRepository>();
        }

        public async Task<CatalogeState> DeleteClient(Client item)
        {
            return await MakeCallUseCase(item, () =>
            {
                return _repository.DeleteClient(item);
            });
        }
    }
}
