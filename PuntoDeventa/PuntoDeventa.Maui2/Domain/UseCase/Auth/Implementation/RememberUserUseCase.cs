using PuntoDeventa.Data.Repository.Auth;
using PuntoDeventa.Domain.Models;
using PuntoDeventa.Domain.UsesCase.Auth;

namespace PuntoDeventa.Demo.Domain.UsesCase.Auth.Implementation
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
