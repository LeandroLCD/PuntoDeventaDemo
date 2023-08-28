using PuntoDeventa.Domain.Models;

namespace PuntoDeventa.Domain.UsesCase.Auth
{
    internal interface IRememberUserUseCase
    {
        RemembermeUser GetRemembermeUser();

        void SetRemembermeUser(RemembermeUser user);
    }
}
