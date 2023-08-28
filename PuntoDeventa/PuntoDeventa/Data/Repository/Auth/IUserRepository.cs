using PuntoDeventa.Domain.Models;
using PuntoDeventa.UI.Auth.States;
using System.Threading.Tasks;

namespace PuntoDeventa.Data.Repository.Auth
{
    public interface IUserRepository
    {
        UserData GetUserCurren();

        Task<AuthStates> RecoveryPassword(string username);

        RemembermeUser GetIsRememberme();

        void SetIsRememberme(RemembermeUser user);
    }
}
