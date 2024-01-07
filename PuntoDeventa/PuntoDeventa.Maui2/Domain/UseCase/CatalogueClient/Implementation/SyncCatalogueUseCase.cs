using PuntoDeventa.Data.Repository.CatalogueClient;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.Domain.UseCase.CatalogueClient.Implementation
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
