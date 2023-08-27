using PuntoDeventa.Domain.Helpers;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace PuntoDeventa.Domain.UseCase.Auth.Implementation
{
    internal class BaseAuthUseCase
    {
        public async Task<AuthStates> MakeCallUseCase(object model, Func<Task<AuthStates>> func)
        {
            var isValid = model.DataAnotationsValid();

            if (isValid.IsNotNull())
            {
                return new AuthStates.Error(string.Join("-", isValid.Select(e => e.Message).ToList()));
            }

            return await func.Invoke();
        }
    }
}
