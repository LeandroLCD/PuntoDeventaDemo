using Newtonsoft.Json;
using PuntoDeventa.Core.LocalData;
using PuntoDeventa.Data.DTO;
using PuntoDeventa.Data.Mappers;
using PuntoDeventa.Data.Models.Errors;
using PuntoDeventa.Domain;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.Models;
using PuntoDeVenta.Domain.Models;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeventa.Data.Repository.Auth
{
    internal class AuthRepository : BaseRepository, IAuthRepository, IUserRepository
    {

        #region Fields
        private HttpClient _httpClient;
        private IDataPreferences _dataPreferences;



        #endregion

        #region Constructor
        public AuthRepository(IDataPreferences dataPreferences)
        {
            _httpClient = new HttpClient()
            {
                Timeout = new TimeSpan(0, 0, 30)
            };
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

            ResultType<UserDataDTO> makeCallResult = await MakeCallNetwork<UserDataDTO>(async () =>
            {
                string body = JsonConvert.SerializeObject(new AuthUserDataDTO(email, password));
                StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
                return await _httpClient.PostAsync(GetUri(path), content);

            });

            if (makeCallResult.Success)
            {
                //TODO
                //Save User Task.run{}
                _dataPreferences.SetUserData(makeCallResult.Data);
                return new AuthStates.Success(makeCallResult.Data.ToUserData());
            }
            else
            {
                string message = CatchError(makeCallResult.Errors.FirstOrDefault().Message);
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
            return new Uri(Path.Combine(Properties.Resources.BaseUrlAuth, $"{path}={Properties.Resources.KeyApplication}"));
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
            return _dataPreferences.GetRemembermeUser().ToRemembermeUser();
        }

        public void SetIsRememberme(RemembermeUser user)
        {
            _dataPreferences.SetRemembermeUser(user.ToRemembermeUserDTO());
        }




        #endregion

    }
}
