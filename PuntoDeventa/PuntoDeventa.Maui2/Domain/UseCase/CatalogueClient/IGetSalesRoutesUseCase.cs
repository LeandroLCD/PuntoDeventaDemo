using PuntoDeventa.UI.CatalogueClient.States;

namespace PuntoDeventa.Domain.UseCase.CatalogueClient
{
    public interface IGetSalesRoutesUseCase
    {
        CatalogeState Get(string id);
    }
}
