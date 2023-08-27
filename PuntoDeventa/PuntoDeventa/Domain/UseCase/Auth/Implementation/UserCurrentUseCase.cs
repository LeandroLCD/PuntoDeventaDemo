using PuntoDeventa.Data.Repository.Auth;
using PuntoDeventa.Domain.Models;
using PuntoDeVenta.Domain.UsesCase.Auth;

namespace PuntoDeVenta.Demo.Domain.UsesCase.Auth.Implementation
{
    internal class UserCurrentUseCase : IUserCurrentUseCase
    {
        private IUserRepository _userRepository;

        public UserCurrentUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;


        }
        public UserData GetUserData()
        {
            return _userRepository.GetUserCurren();
        }
    }
}
