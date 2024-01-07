using PuntoDeVenta.Maui.Domain.Helpers;
using PuntoDeVenta.Maui.UI.Auth.States;

namespace PuntoDeVenta.Maui.Domain.UseCase.Auth.Implementation
{
    internal class BaseAuthUseCase
    {
        public async Task<AuthStates> MakeCallUseCase(object model, Func<Task<AuthStates>> func)
        {
            var isValid = model.DataAnotationsValid();

            if (isValid.IsNotNull())
            {
                return new AuthStates.Error(string.Join(Environment.NewLine, isValid.Select(e => $".- {e.Message}").ToList()));
            }

            return await func.Invoke();
        }
    }
}
