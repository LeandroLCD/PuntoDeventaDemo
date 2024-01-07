namespace PuntoDeventa.Domain.UseCase.CatalogueClient
{
    using PuntoDeventa.UI.CatalogueClient.Model;
    using PuntoDeventa.UI.CatalogueClient.States;
    using System.Threading.Tasks;
    public interface IAddClientUseCase
    {
        Task<CatalogeState> Insert(Client client);
    }
}
