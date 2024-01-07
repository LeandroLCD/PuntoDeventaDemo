using Microsoft.Maui;
namespace PuntoDeventa.Domain.UseCase.CatalogueClient.Implementation
{
    using PuntoDeventa.Data.Repository.CatalogueClient;
    using PuntoDeventa.UI.CatalogueClient.Models;
    using PuntoDeventa.UI.CatalogueClient.States;
    using System.Threading.Tasks;
    using Xamarin.Forms;

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
