﻿using Newtonsoft.Json;
using PuntoDeVenta.Maui.Core.LocalData.Preferences;
using PuntoDeVenta.Maui.Core.Network.HttpFactory;
using PuntoDeVenta.Maui.Data.DTO.Auth;
using PuntoDeVenta.Maui.Data.Mappers;
using PuntoDeVenta.Maui.Data.Models.Errors;
using PuntoDeVenta.Maui.Domain.Helpers;
using PuntoDeVenta.Maui.Domain.Models;
using PuntoDeVenta.Maui.UI.Auth.States;
using System.Text;

namespace PuntoDeVenta.Maui.Data.Repository.Auth
{
    internal class AuthRepository : BaseRepository, IAuthRepository, IUserRepository
    {

        #region Fields
        private HttpClient _httpClient;
        private IDataPreferences _dataPreferences;



        #endregion

        #region Constructor
        public AuthRepository(IDataPreferences dataPreferences, ICustomHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AutnHttp");
            _dataPreferences = dataPreferences;
        }
        #endregion

        #region Methods

        public async Task<AuthStates> Login(string email, string password)
        {
            return await LoginOrRegister(email, password, $"accounts:signInWithPassword?key");
        }


        public async Task<AuthStates> Register(string email, string password)
        {
            return await LoginOrRegister(email, password, $"accounts:signUp?key");
        }
        public Task<bool> Logout()
        {
            throw new NotImplementedException();
        }
        private async Task<AuthStates> LoginOrRegister(string email, string password, string path)
        {

            var makeCallResult = await MakeCallNetwork<UserDataDTO>(async () =>
            {
                 var body = JsonConvert.SerializeObject(new AuthUserDataDTO(email, password));
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                return await _httpClient.PostAsync(GetUri(path), content);

            });

            if (makeCallResult.Success)
            {
                _dataPreferences.SetUserData(makeCallResult.Data);
                return new AuthStates.Success(makeCallResult.Data.ToUserData());
            }
            else
            {
                var message = CatchError(makeCallResult.Errors.FirstOrDefault()?.Message);
                return new AuthStates.Error(message);
            }
        }
        private string CatchError(string message)
        {
            var error = JsonConvert.DeserializeObject<ErrorAuth>(message);

            if (error.IsNotNull())
            {
                if (error.Error.Message.Contains("INVALID_PASSWORD"))
                {
                    return "La contraseña no es válida o el usuario no tiene contraseña.";
                }
                else if (error.Error.Message.Contains("EMAIL_NOT_FOUND"))
                {
                    return " No existe registro de usuario correspondiente a este email. Es posible que el usuario haya sido eliminado.";
                }
                else if (error.Error.Message.Contains("USER_DISABLED"))
                {
                    return "La cuenta de usuario ha sido deshabilitada por un administrador.";
                }
                else
                {
                    return error.Error.Message;
                }
            }
            else
            {
                return "Error No controlado (Falla en la deserealización del json).";
            }

        }

        private Uri GetUri(string path)
        {
            var baseUrl = Properties.Resources.BaseUrlAuth;
            return new Uri(Path.Combine(baseUrl, $"{path}={Properties.Resources.KeyApplication}"));
        }

        public UserData GetUserCurren()
        {
            return _dataPreferences.GetUserData().ToUserData();
        }

        public Task<AuthStates> RecoveryPassword(string username)
        {
            throw new NotImplementedException();
        }

        public RemembermeUser GetIsRememberme()
        {
            return _dataPreferences.GetRememberMeUser().ToRemembermeUser();
        }

        public void SetIsRememberme(RemembermeUser user)
        {
            _dataPreferences.SetRememberMeUser(user.ToRemembermeUserDTO());
        }




        #endregion

    }
}
