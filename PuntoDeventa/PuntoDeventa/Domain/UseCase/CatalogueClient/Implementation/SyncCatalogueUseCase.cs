using PuntoDeventa.Data.Repository.CatalogueClient;
using Xamarin.Forms;

namespace PuntoDeventa.Domain.UseCase.CatalogueClient.Implementation
{
    internal class SyncCatalogueUseCase : ISyncCatalogueUseCase
    {
        private ICatalogueClienteRepository _repository;

        public SyncCatalogueUseCase()
        {
            _repository = DependencyService.Get<ICatalogueClienteRepository>();
        }

        public virtual async void Sync()
        {
             await _repository.Sync();
        }
    }
}
