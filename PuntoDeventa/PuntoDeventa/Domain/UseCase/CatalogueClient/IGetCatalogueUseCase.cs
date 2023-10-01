namespace PuntoDeventa.Domain.UseCase.CatalogueClient
{
    using PuntoDeventa.UI.CatalogueClient.Model;
    using System.Collections.Generic;

    public interface IGetCatalogueUseCase
    {
        IAsyncEnumerable<SalesRoutes> Get();
    }
}
