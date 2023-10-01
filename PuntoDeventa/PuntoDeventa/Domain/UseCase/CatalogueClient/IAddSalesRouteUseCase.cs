namespace PuntoDeventa.Domain.UseCase.CatalogueClient
{
    using PuntoDeventa.UI.CatalogueClient.Model;
    using PuntoDeventa.UI.CatalogueClient.States;
    using PuntoDeventa.UI.CategoryProduct.States;
    using System.Threading.Tasks;
    public interface IAddSalesRouteUseCase
    {
        Task<CatalogeState> Insert(SalesRoutes route);
    }
}
