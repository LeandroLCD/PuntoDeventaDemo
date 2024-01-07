using PuntoDeVenta.Maui.Data.Repository.CatalogueClient;

namespace PuntoDeVenta.Maui.Domain.UseCase.CatalogueClient.Implementation
{
    internal class SyncCatalogueUseCase : ISyncCatalogueUseCase
    {
        private ICatalogueClientRepository _repository;

        public SyncCatalogueUseCase()
        {
            _repository = DependencyService.Get<ICatalogueClientRepository>();
        }

        public virtual async void Sync()
        {
            await _repository.Sync();
        }
    }
}
