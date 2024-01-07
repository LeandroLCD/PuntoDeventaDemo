
using PuntoDeVenta.Maui.UI.Auth.Models;
using PuntoDeVenta.Maui.UI.Auth.States;
using System.Threading.Tasks;

namespace PuntoDeVenta.Maui.Domain.UseCase.Auth
{
    public interface ILoginUseCase
    {
        Task<AuthStates> Login(AuthDataUser model);
    }
}
