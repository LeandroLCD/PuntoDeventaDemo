namespace PuntoDeVenta.Maui.Domain.UseCase.CatalogueClient.Implementation
{
    using PuntoDeVenta.Maui.Data.Repository.CatalogueClient;
    using PuntoDeVenta.Maui.UI.CatalogueClient.Model;
    using PuntoDeVenta.Maui.UI.CatalogueClient.States;
    using System.Threading.Tasks;
    internal class DeleteClientUseCase : BaseCatalogueClientUseCase, IDeleteClientUseCase
    {
        private readonly ICatalogueClientRepository _repository;

        public DeleteClientUseCase()
        {
            _repository = DependencyService.Get<ICatalogueClientRepository>();
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
