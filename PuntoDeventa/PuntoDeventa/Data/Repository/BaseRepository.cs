using Newtonsoft.Json;
using PuntoDeventa.Core.LocalData.Preferences;
using PuntoDeventa.Core.Network;
using PuntoDeventa.Data.DTO.Auth;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.Domain.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PuntoDeventa.Data.Repository
{
    internal class BaseRepository
    {
        //TODO Revisar errores de deserealización.
        public async Task<ResultType<T>> MakeCallNetwork<T>(Func<Task<HttpResponseMessage>> apiCallFunction)
        {
            ResultType<T> resultType = new ResultType<T>();
            try
            {
                var resp = await Task.Run(() => apiCallFunction());

                string jsonResult = await resp.Content.ReadAsStringAsync();

                resultType.Success = resp.StatusCode.Equals(HttpStatusCode.OK);

                switch (resp.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var data = JsonConvert.DeserializeObject<T>(jsonResult);
                        if (data.IsNotNull())
                        {
                            resultType.Data = data;
                        }
                        break;

                    case HttpStatusCode.Unauthorized:
                        var token = await Task.Run(() => TokenRefresf());
                        if (token.IsCompleted)
                        {
                            await MakeCallNetwork<T>(apiCallFunction);
                        }
                        break;

                    default:
                        resultType.Errors.Add(new ErrorMessage("Error No Controlado", jsonResult));
                        break;
                }


            }
            catch (Exception e)
            {
                resultType.Success = false;
                resultType.Errors.Add(new ErrorMessage(e.GetHashCode().ToString(), e.Message));
            }

            return resultType;
        }

        public async Task<ResultType<T>> MakeCallNetwork<T>(HttpResponseMessage HttpResponse)
        {
            ResultType<T> resultType = new ResultType<T>();
            try
            {

                string jsonResult = await HttpResponse.Content.ReadAsStringAsync();

                resultType.Success = HttpResponse.StatusCode.Equals(HttpStatusCode.OK);

                switch (HttpResponse.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var data = JsonConvert.DeserializeObject<T>(jsonResult);
                        if (data.IsNotNull())
                        {
                            resultType.Data = data;
                        }
                        break;

                    default:
                        resultType.Errors.Add(new ErrorMessage("Error No Controlado", jsonResult));
                        break;
                }


            }
            catch (Exception e)
            {
                resultType.Errors.Add(new ErrorMessage(e.GetHashCode().ToString(), e.Message));
            }

            return resultType;
        }

        private async Task<Task> TokenRefresf()
        {
            var dataStore = DependencyService.Get<IDataStore>();
            var dataPreferences = DependencyService.Get<IDataPreferences>();
            var userCurren = dataPreferences.GetUserData();

            var dto = new RefreshTokenDTO(userCurren.RefreshToken);

            var resp = await dataStore.PostAsync(dto, new Uri(Path.Combine(Properties.Resources.BaseUrlAuth, $"token?key={Properties.Resources.KeyApplication}")));

            string jsonResult = await resp.Content.ReadAsStringAsync();

            switch (resp.StatusCode)
            {
                case HttpStatusCode.OK:
                    var data = JsonConvert.DeserializeObject<RefeshTokenResponseDTO>(jsonResult);
                    if (data.IsNotNull())
                    {
                        userCurren.IdToken = data.IdToken;
                        dataPreferences.SetUserData(userCurren);
                        return Task.CompletedTask;
                    }
                    return Task.FromException(new Exception(jsonResult));

                default:
                    return Task.FromException(new Exception(resp.ReasonPhrase));
            }

        }
    }
}
