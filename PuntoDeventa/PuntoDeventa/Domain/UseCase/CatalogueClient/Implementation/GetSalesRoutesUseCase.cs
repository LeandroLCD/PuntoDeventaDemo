using PuntoDeventa.Data.Repository.CatalogueClient;
using PuntoDeventa.UI.CatalogueClient.States;
using Xamarin.Forms;

namespace PuntoDeventa.Domain.UseCase.CatalogueClient.Implementation
{
    internal class GetSalesRoutesUseCase : IGetSalesRoutesUseCase
    {
        private readonly ICatalogueClientRepository _repository;

        public GetSalesRoutesUseCase()
        {
            _repository = DependencyService.Get<ICatalogueClientRepository>();
        }
        public CatalogeState Get(string id)
        {
            return _repository.GetSalesRoutes(id);
        }
    }
}
