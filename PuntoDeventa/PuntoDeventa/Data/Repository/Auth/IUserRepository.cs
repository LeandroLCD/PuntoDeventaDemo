using PuntoDeventa.Domain.Models;
using PuntoDeVenta.Domain.Models;
using PuntoDeventa.Domain;
using System;
using System.Collections.Generic;
using System.Text;
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
