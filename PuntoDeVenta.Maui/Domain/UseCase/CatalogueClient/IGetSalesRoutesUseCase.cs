using PuntoDeVenta.Maui.UI.CatalogueClient.States;

namespace PuntoDeVenta.Maui.Domain.UseCase.CatalogueClient
{
    public interface IGetSalesRoutesUseCase
    {
        CatalogeState Get(string id);
    }
}
