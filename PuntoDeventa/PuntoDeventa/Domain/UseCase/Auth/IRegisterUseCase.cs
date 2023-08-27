using PuntoDeventa.UI.Auth.Models;
using System.Threading.Tasks;

namespace PuntoDeventa.Domain.UseCase.Auth
{
    public interface IRegisterUseCase
    {
        Task<AuthStates> Register(AuthDataUser model);
    }
}
