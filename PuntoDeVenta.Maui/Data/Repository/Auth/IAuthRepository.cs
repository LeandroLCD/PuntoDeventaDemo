using PuntoDeVenta.Maui.UI.Auth.States;

namespace PuntoDeVenta.Maui.Data.Repository.Auth
{
    public interface IAuthRepository
    {
        Task<AuthStates> Login(string email, string password);

        Task<AuthStates> Register(string email, string password);

        Task<bool> Logout();
    }
}
