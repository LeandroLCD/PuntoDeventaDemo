using PuntoDeventa.Domain;
using PuntoDeventa.Domain.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeventa.Data.Repository.Auth
{
    public interface IAuthRepository
    {
        Task<AuthStates> Login(string email, string password);

        Task<AuthStates> Register(string email, string password);

        Task<bool> Logout();

        UserData GetUserCurren();
    }
}
