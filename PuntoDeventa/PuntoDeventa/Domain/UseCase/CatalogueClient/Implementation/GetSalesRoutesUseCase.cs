using PuntoDeventa.Data.Repository.CatalogueClient;
using PuntoDeventa.UI.CatalogueClient.States;
using Xamarin.Forms;

namespace PuntoDeventa.Domain.UseCase.CatalogueClient.Implementation
{
    internal class GetSalesRoutesUseCase : IGetSalesRoutesUseCase
    {
        private readonly ICatalogueClienteRepository _repository;

        public GetSalesRoutesUseCase()
        {
            _repository = DependencyService.Get<ICatalogueClienteRepository>();
        }
        public CatalogeState Get(string id)
        {
            return _repository.GetSalesRoutes(id);
        }
    }
}
