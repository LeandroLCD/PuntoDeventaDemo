using PuntoDeventa.Data.Repository.CatalogueClient;
using PuntoDeventa.UI.CatalogueClient.States;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

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
