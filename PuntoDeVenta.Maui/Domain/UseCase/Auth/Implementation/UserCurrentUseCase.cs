using PuntoDeVenta.Maui.Data.Repository.Auth;
using PuntoDeVenta.Maui.Domain.Models;
using PuntoDeVenta.Maui.Domain.UsesCase.Auth;

namespace PuntoDeVenta.Maui.Demo.Domain.UsesCase.Auth.Implementation
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
