using PuntoDeVenta.Maui.Domain.Helpers;
using PuntoDeVenta.Maui.UI.CatalogueClient.States;
namespace PuntoDeVenta.Maui.Domain.UseCase.CatalogueClient
{
    using System.Threading.Tasks;
    internal class BaseCatalogueClientUseCase
    {
        public async Task<CatalogeState> MakeCallUseCase(object model, Func<Task<CatalogeState>> func)
        {
            var isValid = model.DataAnotationsValid();

            if (isValid.IsNotNull())
            {
                return new CatalogeState.Error(string.Join(Environment.NewLine, isValid.Select(e => $".* {e.Message}").ToList()));
            }

            return await func.Invoke();
        }
    }
}
