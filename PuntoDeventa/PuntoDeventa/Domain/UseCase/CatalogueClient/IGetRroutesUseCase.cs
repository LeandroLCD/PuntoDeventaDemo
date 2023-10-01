namespace PuntoDeventa.Domain.UseCase.CatalogueClient
{
    using PuntoDeventa.UI.CatalogueClient.Model;
    using System.Collections.Generic;
    using System.Threading;

    public interface IGetRroutesUseCase
    {
        IAsyncEnumerable<List<SalesRoutes>> Emit(CancellationToken token, int inMilliseconds = 500);
    }
}
