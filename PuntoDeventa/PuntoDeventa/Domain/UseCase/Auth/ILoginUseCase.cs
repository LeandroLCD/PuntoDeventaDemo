using PuntoDeventa.UI.Auth.Models;
using PuntoDeventa.UI.Auth.States;
using System.Threading.Tasks;

namespace PuntoDeventa.Domain.UseCase.Auth
{
    public interface ILoginUseCase
    {
        Task<AuthStates> Login(AuthDataUser model);
    }
}
