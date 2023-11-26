using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.UI.Sales.State;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PuntoDeventa.Domain.UseCase.Sales.Implementation
{
    internal class BaseSalesUseCase
    {
        public async Task<SalesState> MakeCallUseCase(object model, Func<Task<SalesState>> func)
        {
            var isValid = model.DataAnotationsValid();

            if (isValid.IsNotNull())
            {
                return SalesState.Error.Instance(string.Join(Environment.NewLine, isValid.Select(e => $".* {e.Message}").ToList()));
            }

            return await func.Invoke();
        }
    }
}
