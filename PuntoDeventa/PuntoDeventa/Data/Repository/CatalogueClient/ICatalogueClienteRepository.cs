using PuntoDeventa.UI.CatalogueClient.Model;
using PuntoDeventa.UI.CatalogueClient.Models;
using PuntoDeventa.UI.CatalogueClient.States;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PuntoDeventa.Data.Repository.CatalogueClient
{
    public interface ICatalogueClienteRepository
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
    }
}
