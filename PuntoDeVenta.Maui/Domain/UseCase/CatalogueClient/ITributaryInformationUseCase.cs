namespace PuntoDeVenta.Maui.Domain.UseCase.CatalogueClient
{
    using PuntoDeVenta.Maui.UI.CatalogueClient.Models;
    using PuntoDeVenta.Maui.UI.CatalogueClient.States;
    using System.Threading.Tasks;

    public interface ITributaryInformationUseCase
    {
        Task<CatalogeState> Get(Rut rut);
    }
}
