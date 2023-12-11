namespace PuntoDeventa.Domain.UseCase.CatalogueClient
{
    using PuntoDeventa.UI.CatalogueClient.Model;
    using System.Collections.Generic;
    using System.Threading;

    public interface IGetRoutesUseCase
    {
        IEnumerable<SalesRoutes> Emit(CancellationToken token);

        IAsyncEnumerable<List<SalesRoutes>> Emit(CancellationToken token, int inMilliseconds = 500);
    }
}
