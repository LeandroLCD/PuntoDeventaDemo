using PuntoDeVenta.Maui.Data.Repository.Auth;
using PuntoDeVenta.Maui.Domain.UseCase.Auth;
using PuntoDeVenta.Maui.Domain.UseCase.Auth.Implementation;
using PuntoDeVenta.Maui.UI.Auth.Models;
using PuntoDeVenta.Maui.UI.Auth.States;
using System.Threading.Tasks;

namespace PuntoDeVenta.Maui.Demo.Domain.UsesCase.Auth.Implementation
{
    internal class RegisterUseCase : BaseAuthUseCase, IRegisterUseCase
    {
        private IAuthRepository _repository;

        public RegisterUseCase(IAuthRepository authRepository)
        {
            _repository = authRepository;
        }
        public async Task<AuthStates> Register(AuthDataUser model)
        {
            return await MakeCallUseCase(model, async () =>
            {
                return await _repository.Register(model.Email, model.Password);
            });

        }
    }
}
