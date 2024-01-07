using PuntoDeVenta.Maui.Domain.Models;

namespace PuntoDeVenta.Maui.Domain.UsesCase.Auth
{
    public interface IUserCurrentUseCase
    {
        UserData GetUserData();
    }
}
