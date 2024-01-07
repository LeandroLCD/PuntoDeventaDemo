namespace PuntoDeVenta.Maui.Domain.UseCase.CatalogueClient.Implementation
{
    using PuntoDeVenta.Maui.Data.Repository.CatalogueClient;
    using PuntoDeVenta.Maui.UI.CatalogueClient.Models;
    using PuntoDeVenta.Maui.UI.CatalogueClient.States;
    using System.Threading.Tasks;

    internal class TributaryInformationUseCase : BaseCatalogueClientUseCase, ITributaryInformationUseCase
    {
        private readonly ICatalogueClientRepository _repository;

        public TributaryInformationUseCase()
        {
            _repository = DependencyService.Get<ICatalogueClientRepository>();
        }
        public async Task<CatalogeState> Get(Rut rut)
        {
            return await MakeCallUseCase(rut, async () =>
            {
                return await _repository.GetTributaryInformation(rut);
            });
        }
    }
}
