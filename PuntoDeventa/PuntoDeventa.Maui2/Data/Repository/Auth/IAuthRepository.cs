using PuntoDeventa.UI.Auth.States;
using System.Threading.Tasks;

namespace PuntoDeventa.Data.Repository.Auth
{
    public interface IAuthRepository
    {
        Task<AuthStates> Login(string email, string password);

        Task<AuthStates> Register(string email, string password);

        Task<bool> Logout();
    }
}
