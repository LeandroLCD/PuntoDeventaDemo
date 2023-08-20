using PuntoDeventa.Domain.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeventa.Data.Repository.Auth
{
    public interface IAuthRepository
    {
        Task<Response<UserData>> Login(string username, string password);
    }
}
