using PuntoDeVenta.Maui.Domain.Models;

namespace PuntoDeVenta.Maui.Domain.UsesCase.Auth
{
    internal interface IRememberUserUseCase
    {
        RemembermeUser GetRemembermeUser();

        void SetRemembermeUser(RemembermeUser user);
    }
}
