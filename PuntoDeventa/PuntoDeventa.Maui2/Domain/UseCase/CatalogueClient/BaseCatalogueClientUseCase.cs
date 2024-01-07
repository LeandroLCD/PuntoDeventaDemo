using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.UI.CatalogueClient.States;
using System;
using System.Linq;
namespace PuntoDeventa.Domain.UseCase.CatalogueClient
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
