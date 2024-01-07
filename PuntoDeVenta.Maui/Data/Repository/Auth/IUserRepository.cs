using PuntoDeVenta.Maui.Domain.Models;
using PuntoDeVenta.Maui.UI.Auth.States;

namespace PuntoDeVenta.Maui.Data.Repository.Auth
{
    public interface IUserRepository
    {
        UserData GetUserCurren();

        Task<AuthStates> RecoveryPassword(string username);

        RemembermeUser GetIsRememberme();

        void SetIsRememberme(RemembermeUser user);
    }
}
