using PuntoDeventa.Domain.Models;

namespace PuntoDeventa.Domain.UsesCase.Auth
{
    public interface IUserCurrentUseCase
    {
        UserData GetUserData();
    }
}
