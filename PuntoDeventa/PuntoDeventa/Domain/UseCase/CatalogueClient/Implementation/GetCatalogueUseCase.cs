using PuntoDeventa.Data.Repository.CatalogueClient;
using PuntoDeventa.UI.CatalogueClient.Model;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PuntoDeventa.Domain.UseCase.CatalogueClient.Implementation
{
    internal class GetCatalogueUseCase: IGetCatalogueUseCase
    {
        private ICatalogueClienteRepository _repository;

        public GetCatalogueUseCase()
        {
            _repository = DependencyService.Get<ICatalogueClienteRepository>();
        }

        public IAsyncEnumerable<SalesRoutes> Get()
        {
            return _repository.GetCatalogueAsync();
        }
    }
}
