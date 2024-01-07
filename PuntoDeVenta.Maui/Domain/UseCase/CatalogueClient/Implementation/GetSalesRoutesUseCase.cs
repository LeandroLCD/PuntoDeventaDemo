using PuntoDeVenta.Maui.Data.Repository.CatalogueClient;
using PuntoDeVenta.Maui.UI.CatalogueClient.States;

namespace PuntoDeVenta.Maui.Domain.UseCase.CatalogueClient.Implementation
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
