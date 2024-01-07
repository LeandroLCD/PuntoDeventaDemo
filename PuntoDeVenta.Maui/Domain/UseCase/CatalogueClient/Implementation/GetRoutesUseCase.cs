namespace PuntoDeVenta.Maui.Domain.UseCase.CatalogueClient.Implementation
{
    using PuntoDeVenta.Maui.Data.Repository.CatalogueClient;
    using PuntoDeVenta.Maui.UI.CatalogueClient.Model;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    internal class GetRroutesUseCase : IGetRoutesUseCase
    {
        private ICatalogueClientRepository _repository;

        public GetRroutesUseCase()
        {
            _repository = DependencyService.Get<ICatalogueClientRepository>();
        }
        /// <summary>
        /// Retorna un Data Flow de Rutas con sus clientes, requiere un CancellationToken y un tiempo en 
        /// milisegundos que representa el tiempo entre cada llamada.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="inMilisegunds"></param>
        /// <returns></returns>
        public IEnumerable<SalesRoutes> Emit(CancellationToken token)
        {
            while (token.IsCancellationRequested.Equals(false))
            {
                //await Task.Delay(inMilliseconds);
                var list = _repository.GetRoutesAll();
                foreach (var route in list)
                {
                    yield return route;
                }
            }
        }
        public async IAsyncEnumerable<List<SalesRoutes>> Emit([EnumeratorCancellation] CancellationToken token, int inMilliseconds = 500)
        {
            while (token.IsCancellationRequested.Equals(false))
            {
                await Task.Delay(inMilliseconds, token);
                yield return _repository.GetRoutesAll();
            }
        }
    }
}
