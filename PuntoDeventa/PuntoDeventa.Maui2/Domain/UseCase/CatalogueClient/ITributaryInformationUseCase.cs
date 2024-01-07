namespace PuntoDeventa.Domain.UseCase.CatalogueClient
{
    using PuntoDeventa.UI.CatalogueClient.Models;
    using PuntoDeventa.UI.CatalogueClient.States;
    using System.Threading.Tasks;

    public interface ITributaryInformationUseCase
    {
        Task<CatalogeState> Get(Rut rut);
    }
}
