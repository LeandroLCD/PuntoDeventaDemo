using PuntoDeVenta.Maui.UI.CatalogueClient.Model;
using PuntoDeVenta.Maui.UI.CatalogueClient.Models;
using PuntoDeVenta.Maui.UI.CatalogueClient.States;

namespace PuntoDeVenta.Maui.Data.Repository.CatalogueClient
{
    public interface ICatalogueClientRepository
    {
        Task<CatalogeState> InsertRoute(SalesRoutes item);
        Task<CatalogeState> InsertClient(Client item);

        Task<CatalogeState> DeleteRoute(SalesRoutes item);
        Task<CatalogeState> DeleteClient(Client item);

        Task<CatalogeState> UpDateClient(Client item);

        Task<CatalogeState> GetTributaryInformation(Rut rut);

        List<SalesRoutes> GetRoutesAll();

        IAsyncEnumerable<SalesRoutes> GetCatalogueAsync();
        CatalogeState GetSalesRoutes(string id);

        Task<bool> Sync();
    }
}
