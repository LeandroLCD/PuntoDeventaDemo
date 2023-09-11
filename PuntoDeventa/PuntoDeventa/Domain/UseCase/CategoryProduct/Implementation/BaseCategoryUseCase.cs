using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.UI.CategoryProduct.States;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PuntoDeventa.Domain.UseCase.CategoryProduct.Implementation
{
    internal class BaseCategoryUseCase
    {
        public async Task<CategoryStates> MakeCallUseCase(object model, Func<Task<CategoryStates>> func)
        {
            var isValid = model.DataAnotationsValid();

            if (isValid.IsNotNull())
            {
                return new CategoryStates.Error(string.Join(Environment.NewLine, isValid.Select(e => $".* {e.Message}").ToList()));
            }

            return await func.Invoke();
        }
    }
}
