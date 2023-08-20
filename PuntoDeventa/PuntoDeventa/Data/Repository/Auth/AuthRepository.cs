using Newtonsoft.Json;
using PuntoDeventa.Data.DTO;
using PuntoDeventa.Data.Mappers;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.Helpers.Models;
using PuntoDeventa.UI.Auth.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeventa.Data.Repository.Auth
{
    internal class AuthRepository : IAuthRepository
    {

        #region Fields
        private HttpClient _httpClient;



        #endregion

        #region Constructor
        public AuthRepository()
        {
            _httpClient = new HttpClient()
            {
                Timeout = new TimeSpan(0, 0, 30)
            };
        }
        #endregion

        #region Methods

        public async Task<Response<UserData>> Login(string email, string password)
        {
            var user = new AuthUserDataDTO()
            {
                Email = email,
                Password = password,
                ReturnSecureToken = true
            };
            try
            {
                string body = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(body, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{Properties.Resources.BaseUrlAuth}accounts:signInWithPassword?key={Properties.Resources.KeyApplication}", content);
                string jsonResult = await response.Content.ReadAsStringAsync();
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    var dto = JsonConvert.DeserializeObject<UserDataDTO>(jsonResult);
                    if(dto.IsNotNull())
                    {
                        return new Response<UserData>()
                        {
                            Success = true,
                            Message = "Cualquier cosa",
                            StatusCode = response.StatusCode.GetHashCode(),
                            Data = dto.ToUserData()
                        };
                    }

                }
            }
            catch (Exception ex)
            {
                var t = ex.ToString();
            }


            return new Response<UserData>();


        }

        public Task<Response<bool>> Logout()
        {
            throw new NotImplementedException();
        }

        public Task<Response<AuthDataUser>> Register(AuthDataUser model)
        {
            throw new NotImplementedException();
        }


        #endregion

    }
}
