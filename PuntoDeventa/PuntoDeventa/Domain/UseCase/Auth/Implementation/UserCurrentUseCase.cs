using PuntoDeventa.Data.Repository.Auth;
using PuntoDeventa.Domain.Models;
using PuntoDeventa.Domain.UsesCase.Auth;

namespace PuntoDeventa.Demo.Domain.UsesCase.Auth.Implementation
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
