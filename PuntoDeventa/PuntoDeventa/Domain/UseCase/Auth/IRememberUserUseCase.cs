using PuntoDeVenta.Domain.Models;

namespace PuntoDeVenta.Domain.UsesCase.Auth
{
    internal interface IRememberUserUseCase
    {
        RemembermeUser GetRemembermeUser();

        void SetRemembermeUser(RemembermeUser user);
    }
}
