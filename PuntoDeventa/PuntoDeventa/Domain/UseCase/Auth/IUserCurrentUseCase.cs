using PuntoDeventa.Domain.Models;

namespace PuntoDeVenta.Domain.UsesCase.Auth
{
    public interface IUserCurrentUseCase
    {
        UserData GetUserData();
    }
}
