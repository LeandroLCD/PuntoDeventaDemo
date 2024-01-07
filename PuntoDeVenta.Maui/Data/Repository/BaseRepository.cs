using Newtonsoft.Json;
using PuntoDeVenta.Maui.Core.LocalData.Preferences;
using PuntoDeVenta.Maui.Core.Network;
using PuntoDeVenta.Maui.Data.DTO.Auth;
using PuntoDeVenta.Maui.Data.DTO.EmissionSystem.Error;
using PuntoDeVenta.Maui.Data.Models;
using PuntoDeVenta.Maui.Domain.Helpers;
using PuntoDeVenta.Maui.Domain.Models;
using System.Net;

namespace PuntoDeVenta.Maui.Data.Repository
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

                var jsonResult = await resp.Content.ReadAsStringAsync();

                resultType.Success = resp.StatusCode.Equals(HttpStatusCode.OK);

                switch (resp.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var data = JsonConvert.DeserializeObject<T>(jsonResult);
                        if (data.IsNotNull())
                        {
                            resultType.Data = data;
                            break;
                        }

                        resultType.Success = false;

                        resultType.Errors.Add(new ErrorMessage("Error", "no se pudo desereaizar la respuesta."));

                        resultType.Errors.Add(new ErrorMessage("response", jsonResult));
                        break;

                    case HttpStatusCode.Unauthorized:
                        
                        var token = await Task.Run(() => TokenRefresf());
                        if (token.IsCompleted)
                        {
                            //TODO mejorar logica de reflexion 
                            //await MakeCallNetwork<T>(apiCallFunction);
                        }
                        break;
                    case HttpStatusCode.BadRequest:
                        if (jsonResult.Contains("OF-"))
                        {
                            var error = JsonConvert.DeserializeObject<CatchErrorDTO>(jsonResult);
                            resultType.Errors.Add(error.IsNotNull()
                                ? new ErrorMessage("Error del Facturador de Mercado", error.Error.ToString())
                                : new ErrorMessage("Error No Controlado", jsonResult));
                        }
                        break;

                    case HttpStatusCode.ServiceUnavailable:
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
            var dataStore = Application.Current.Handler.MauiContext.Services.GetServices<IDataStore>().First();
            var dataPreferences = Application.Current.Handler.MauiContext.Services.GetServices<IDataPreferences>().First();


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
