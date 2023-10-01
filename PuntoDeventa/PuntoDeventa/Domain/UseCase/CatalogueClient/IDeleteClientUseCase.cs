namespace PuntoDeventa.Domain.UseCase.CatalogueClient
{
    using PuntoDeventa.UI.CatalogueClient.Model;
    using PuntoDeventa.UI.CatalogueClient.States;
    using System.Threading.Tasks;
    public interface IDeleteClientUseCase
    {
        Task<CatalogeState> DeleteClient(Client item);
    }
}
