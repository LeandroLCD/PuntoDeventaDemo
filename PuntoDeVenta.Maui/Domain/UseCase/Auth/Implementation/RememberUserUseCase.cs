using PuntoDeVenta.Maui.Data.Repository.Auth;
using PuntoDeVenta.Maui.Domain.Models;
using PuntoDeVenta.Maui.Domain.UsesCase.Auth;

namespace PuntoDeVenta.Maui.Demo.Domain.UsesCase.Auth.Implementation
{
    internal class RememberUserUseCase : IRememberUserUseCase
    {
        private IUserRepository _userRepository;

        public RememberUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public RemembermeUser GetRemembermeUser()
        {
            return _userRepository.GetIsRememberme();
        }

        public void SetRemembermeUser(RemembermeUser user)
        {
            _userRepository.SetIsRememberme(user);
        }
    }
}
