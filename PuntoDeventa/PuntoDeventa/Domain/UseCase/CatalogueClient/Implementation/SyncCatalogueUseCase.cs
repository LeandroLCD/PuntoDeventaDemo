using PuntoDeventa.Data.Repository.CatalogueClient;
using PuntoDeventa.UI.CatalogueClient.Model;
using System.Collections.Generic;
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

        public IAsyncEnumerable<SalesRoutes> Sync()
        {
            return _repository.GetCatalogueAsync();
        }
    }
}
