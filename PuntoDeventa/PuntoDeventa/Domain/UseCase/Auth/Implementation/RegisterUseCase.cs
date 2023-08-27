using PuntoDeventa.Data.Repository.Auth;
using PuntoDeventa.Domain;
using PuntoDeventa.Domain.UseCase.Auth;
using PuntoDeventa.Domain.UseCase.Auth.Implementation;
using PuntoDeventa.UI.Auth.Models;
using System.Threading.Tasks;

namespace PuntoDeVenta.Demo.Domain.UsesCase.Auth.Implementation
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
